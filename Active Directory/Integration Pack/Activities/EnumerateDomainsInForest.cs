using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;


namespace Active_Directory
{
    [Activity("Enumerate Domains in Forest")]
    public class EnumerateDomainsInForest : IActivity
    {
        private string forest = string.Empty;
        private ConnectionCredentials credentials;

        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("ForestName").WithDefaultValue("Contoso.com");
            designer.AddCorellatedData(typeof(domain));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            forest = request.Inputs["ForestName"].AsString();
            response.WithFiltering().PublishRange(getChildDomains(forest));
        }

        private IEnumerable<domain> getChildDomains(string forest)
        {
            DirectoryContext directoryContext = new DirectoryContext(DirectoryContextType.Forest,forest,credentials.UserName + "@" + credentials.Domain, credentials.Password);
            ArrayList alDomains = new ArrayList();
            Forest currentForest = Forest.GetForest(directoryContext);
            DomainCollection myDomains = currentForest.Domains;

            foreach (Domain objDomain in myDomains)
            {
                yield return new domain(objDomain.Name);
            }
        }
    }
}

