using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Delete Trust Relationship", ShowFilters = false)]
    class DeleteTrustRelationship : IActivity
    {
        private ConnectionCredentials credentials;

        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Source Forest Name").WithDefaultValue("Contoso.com");
            designer.AddInput("Target Forest Name").WithDefaultValue("TargetForest.com");
            designer.AddInput("Target Forest Username").NotRequired().WithDefaultValue("user@targetforest.com");
            designer.AddInput("Target Forest User Password").NotRequired().PasswordProtect();

            designer.AddOutput("Source Forest Name").AsString();
            designer.AddOutput("Target Forest Name").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String sourceForestName = request.Inputs["Source Forest Name"].AsString();
            String targetForestName = request.Inputs["Target Forest Name"].AsString();

            String TargetForestUsername = String.Empty;
            String TargetForestUserPassword = String.Empty;

            TargetForestUsername = request.Inputs["Target Forest Username"].AsString();
            TargetForestUserPassword = request.Inputs["Target Forest User Password"].AsString();

            Forest sourceForest;
            Forest targetForest;

            sourceForest = Forest.GetForest(new DirectoryContext(DirectoryContextType.Forest, sourceForestName, credentials.UserName + "@" + credentials.Domain, credentials.Password));

            if (TargetForestUsername != String.Empty && TargetForestUserPassword != String.Empty)
            {
                targetForest = Forest.GetForest(new DirectoryContext(DirectoryContextType.Forest, targetForestName, TargetForestUsername, TargetForestUserPassword));
            }
            else
            {
                targetForest = Forest.GetForest(new DirectoryContext(DirectoryContextType.Forest, targetForestName, credentials.UserName + "@" + credentials.Domain, credentials.Password));
            }

            sourceForest.DeleteTrustRelationship(targetForest);

            response.Publish("Source Forest Name", sourceForestName);
            response.Publish("Target Forest Name", targetForestName);
        }
    }
}

