using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCM2012Interop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;

namespace SCCM2012IntegrationPack
{
    [Activity("Modify SCCM Collection: Add Direct Member")]
    public class ModifyCollectionAddDirectMember : IActivity
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
            String[] membershipTypeChoices = new String[] { "System","User","User Group" };
            designer.AddInput("Collection ID").WithDefaultValue("ABC00000");
            designer.AddInput("Direct Membership Type").WithDefaultValue("System").WithListBrowser(membershipTypeChoices);
            designer.AddInput("ResourceID").WithDefaultValue(1);
            
            designer.AddCorellatedData(typeof(collection));
            designer.AddOutput("Number of Collections");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Collection ID"].AsString();
            String membershipType = request.Inputs["Direct Membership Type"].AsString();
            int resourceID = (int)request.Inputs["ResourceID"].AsUInt32();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                switch (membershipType)
                {
                    case "System":
                        CM2012Interop.addDirectSCCMCollectionMemberMachine(connection, objID, resourceID);
                        break;
                    case "User":
                        CM2012Interop.addDirectSCCMCollectionMemberUser(connection, objID, resourceID);
                        break;
                    case "User Group":
                        CM2012Interop.addDirectSCCMCollectionMemberUserGroup(connection, objID, resourceID);
                        break;
                    default:
                        response.LogErrorMessage("Invalid Direct Membership Type");
                        break;
                }

                IResultObject col = null;
                col = CM2012Interop.getSCCMCollection(connection, "CollectionID LIKE '" + objID + "'");

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

