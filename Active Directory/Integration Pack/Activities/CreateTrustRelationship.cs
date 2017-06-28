using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Create Trust Relationship",ShowFilters=false)]
    class CreateTrustRelationship : IActivity
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
            string[] trustRelationDirectionOptions = new string[3];
            trustRelationDirectionOptions[0] = "Outbound";
            trustRelationDirectionOptions[1] = "Inbound";
            trustRelationDirectionOptions[2] = "Bidirectional";

            designer.AddInput("Source Forest Name").WithDefaultValue("Contoso.com");
            designer.AddInput("Target Forest Name").WithDefaultValue("TargetForest.com");
            designer.AddInput("Trust Relationship Direction").WithListBrowser(trustRelationDirectionOptions).WithDefaultValue("Outbound");
            designer.AddInput("Target Forest Username").NotRequired().WithDefaultValue("user@targetforest.com");
            designer.AddInput("Target Forest User Password").NotRequired().PasswordProtect();

            designer.AddOutput("Source Forest Name").AsString();
            designer.AddOutput("Target Forest Name").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String sourceForestName = request.Inputs["Source Forest Name"].AsString();
            String targetForestName = request.Inputs["Target Forest Name"].AsString();
            String trustRelationshipDirection = request.Inputs["Trust Relationship Direction"].AsString();

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

            switch (trustRelationshipDirection)
            {
                case "Outbound":
                    sourceForest.CreateTrustRelationship(targetForest, TrustDirection.Outbound);
                    break;
                case "Inbound":
                    sourceForest.CreateTrustRelationship(targetForest, TrustDirection.Inbound);
                    break;
                case "Bidirectional":
                    sourceForest.CreateTrustRelationship(targetForest, TrustDirection.Bidirectional);
                    break;
                default:
                    sourceForest.CreateTrustRelationship(targetForest, TrustDirection.Outbound);
                    break;
            }

            response.Publish("Source Forest Name", sourceForestName);
            response.Publish("Target Forest Name", targetForestName);
        }
    }
}

