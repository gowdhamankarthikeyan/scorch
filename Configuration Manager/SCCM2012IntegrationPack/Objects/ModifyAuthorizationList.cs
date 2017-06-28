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
    [Activity("Modify SCCM Authorization List")]
    public class ModifyAuthorizationList : IActivity
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
            designer.AddInput("CI_ID").WithDefaultValue("11189");

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(settings.SCCMSERVER, settings.UserName, settings.Password);

            String[] propertyNameChoices = CM2012Interop.getSCCMObjectPropertyNames(connection, "SMS_AuthorizationList");
            String[] propertyTypeChoices = new String[] { "StringValue", "DateTimeValue", "IntegerValue", "BooleanValue" };

            foreach (String propertyName in propertyNameChoices)
            {
                designer.AddInput(propertyName + " : Property Type").WithListBrowser(propertyTypeChoices).WithDefaultValue("StringValue").NotRequired();
                designer.AddInput(propertyName + " : Property Value").NotRequired();
            }

            designer.AddCorellatedData(typeof(authorizationList));
            designer.AddOutput("Number of Authorization Lists");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["CI_ID"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                String[] propertyNameChoices = CM2012Interop.getSCCMObjectPropertyNames(connection, "SMS_AuthorizationList");
                foreach (String propertyName in propertyNameChoices)
                {
                    if ((request.Inputs.Contains(propertyName + " : Property Type")) && (request.Inputs.Contains(propertyName + " : Property Value")))
                    {
                        CM2012Interop.modifySCCMAuthorizationList(connection, objID, request.Inputs[(propertyName + " : Property Type")].AsString(), propertyName, request.Inputs[(propertyName + " : Property Value")].AsString());
                    }
                }

                IResultObject col = null;
                col = CM2012Interop.getSCCMAuthorizationList(connection, "CI_ID LIKE '" + objID + "'");

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Authorization Lists", ObjCount);
            }
        }
        private IEnumerable<authorizationList> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new authorizationList(obj);
            }
        }
    }
}

