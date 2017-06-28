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
    [Activity("Create SCCM Advertisement")]
    public class CreateAdvertisement : IActivity
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
            String[] StartOfferEnabledOptions = new String[] { "True", "False" };
            designer.AddInput("Existing Collection ID").WithDefaultValue("CollectionID");
            designer.AddInput("Existing Package ID").WithDefaultValue("PackageID");
            designer.AddInput("Existing Program Name").WithDefaultValue("Program Name");
            designer.AddInput("New Advertisement Name").WithDefaultValue("Advertisement Name");
            designer.AddInput("New Advertisement Comment").WithDefaultValue("Comment");
            designer.AddInput("New Advertisement Flags").WithDefaultValue(1179648);
            designer.AddInput("New Advertisement Start Date").WithDefaultValue("20080703084600.000000+***");
            designer.AddInput("New Advertisement Start Offer Enabled").WithDefaultValue("True").WithListBrowser(StartOfferEnabledOptions);
            designer.AddInput("New Advertisement Include Sub Collection").WithListBrowser(StartOfferEnabledOptions).WithDefaultValue("True");
            designer.AddCorellatedData(typeof(advertisement));
            designer.AddOutput("Number of Advertisements");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String pkgID = request.Inputs["Existing Package ID"].AsString();
            String prgName = request.Inputs["Existing Program Name"].AsString();
            String colID = request.Inputs["Existing Collection ID"].AsString();
            String advName = request.Inputs["New Advertisement Name"].AsString();
            String advComment = request.Inputs["New Advertisement Comment"].AsString();
            int advFlags = (int)request.Inputs["New Advertisement Flags"].AsUInt32();
            String advStartDate = request.Inputs["New Advertisement Start Date"].AsString();
            String advStartOfferEnabled = request.Inputs["New Advertisement Start Offer Enabled"].AsString();
            String advIncludeSubCollections = request.Inputs["New Advertisement Include Sub Collection"].AsString();

            bool advertStartOfferEnabled = true;

            switch (advStartOfferEnabled)
            {
                case "True":
                    advertStartOfferEnabled = true;
                    break;
                case "False":
                    advertStartOfferEnabled = false;
                    break;
            }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = CM2012Interop.createSCCMAdvertisement(connection, colID, pkgID, prgName, advName, advComment, advFlags, advStartDate, advertStartOfferEnabled);


                //If advetisement should not include subcollection modify that property
                if (advIncludeSubCollections.Equals("False"))
                {
                    CM2012Interop.modifySCCMAdvertisement(connection, col["AdvertisementID"].StringValue, "BooleanValue", "includeSubCollection", "False");
                }

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));
                }
                response.Publish("Number of Advertisements", ObjCount);
            }
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

