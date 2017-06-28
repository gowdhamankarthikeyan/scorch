using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace Active_Directory
{
    [Activity("Get Primary Domain Controller", ShowFilters = false)]
    public class GetPrimaryDomainController : IActivity
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
            designer.AddInput("Domain Name").WithDefaultValue("Contoso.com");
            designer.AddOutput("Primary Domain Controller");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String domainName = request.Inputs["Domain Name"].AsString();
            
            DirectoryContext objContext = new DirectoryContext(DirectoryContextType.Domain, domainName, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            Domain objDomain = Domain.GetDomain(objContext);
            String PDC = objDomain.PdcRoleOwner.ToString();

            response.Publish("Primary Domain Controller", PDC);
        }
    }
}

