using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCMInterop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;


namespace SCCMExtension
{
    [Activity("Modify SCCM Collection: Add Membership Rule")]
    public class ModifyCollectionAddMembershipRule : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Collection ID").WithDefaultValue("ABC00000");
            designer.AddInput("Rule Name").WithDefaultValue("Membership Rule");
            designer.AddInput("WQL Query");
            designer.AddInput("Collection ID to Limit Query To").WithDefaultValue("AAA00000").NotRequired();

            designer.AddCorellatedData(typeof(collection));
            designer.AddOutput("Number of Collections");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Collection ID"].AsString();
            String ruleName = request.Inputs["Rule Name"].AsString();
            String wqlQuery = request.Inputs["WQL Query"].AsString();
            String limitCollectionID = String.Empty;
            if (request.Inputs.Contains("Collection ID to Limit Query To")) { limitCollectionID = request.Inputs["Collection ID to Limit Query To"].AsString(); }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                CMInterop.addSCCMCollectionRule(connection, objID, ruleName, wqlQuery, limitCollectionID);

                IResultObject col = null;
                col = CMInterop.getSCCMCollection(connection, "CollectionID LIKE '" + objID + "'");

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Collections", ObjCount);
            }
        }
        private IEnumerable<collection> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new collection(obj);
            }
        }
    }
}

