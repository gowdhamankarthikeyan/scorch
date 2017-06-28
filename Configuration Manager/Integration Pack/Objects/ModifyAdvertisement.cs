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
    [Activity("Modify SCCM Advertisement")]
    public class ModifyAdvertisement : IActivity
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
            designer.AddInput("Advertisement ID").WithDefaultValue("ABC00000");

            WqlConnectionManager connection = CMInterop.connectSCCMServer(settings.SCCMSERVER, settings.UserName, settings.Password);
            using(connection)
            {  
                String[] propertyNameChoices = CMInterop.getSCCMObjectPropertyNames(connection, "SMS_Advertisement");
                String[] propertyTypeChoices = new String[] { "StringValue", "DateTimeValue", "IntegerValue", "BooleanValue" };

                foreach (String propertyName in propertyNameChoices)
                {
                    designer.AddInput(propertyName + " : Property Type").WithListBrowser(propertyTypeChoices).WithDefaultValue("StringValue").NotRequired();
                    designer.AddInput(propertyName + " : Property Value").NotRequired();
                }

                designer.AddCorellatedData(typeof(advertisement));
                designer.AddOutput("Number of Advertisements");
            }
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Advertisement ID"].AsString();
            
            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            
            String[] propertyNameChoices = CMInterop.getSCCMObjectPropertyNames(connection, "SMS_Advertisement");
            foreach (String propertyName in propertyNameChoices)
            {
                if((request.Inputs.Contains(propertyName + " : Property Type")) && (request.Inputs.Contains(propertyName + " : Property Value")))
                {
                    CMInterop.modifySCCMAdvertisement(connection, objID, request.Inputs[(propertyName + " : Property Type")].AsString(), propertyName, request.Inputs[(propertyName + " : Property Value")].AsString());
                }
            }

            IResultObject col = null;
            col = CMInterop.getSCCMAdvertisement(connection,"AdvertisementID LIKE '" + objID + "'");

            if (col != null)
            {
                response.WithFiltering().PublishRange(getObjects(col));

            }
            response.Publish("Number of Advertisements", ObjCount);
        }
        private IEnumerable<advertisement> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new advertisement(obj);
            }
        }
    }
}

