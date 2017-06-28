using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{ 
    [Activity("Get Object DistinguishedName")]
    public class GetObjectDistinguishedName : IActivity
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
            string[] objClassOptions = new string [5];
            objClassOptions[0] = "user";
            objClassOptions[1] = "group";
            objClassOptions[2] = "computer";
            objClassOptions[3] = "printqueue";
            objClassOptions[4] = "LDAP Search Filter";

            designer.AddInput("DomainName").WithDefaultValue("Contoso.com");
            designer.AddInput("Object Name").WithDefaultValue("ObjectName");
            designer.AddInput("Object Class").WithListBrowser(objClassOptions);

            designer.AddCorellatedData(typeof(ADObject));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String domainName = request.Inputs["DomainName"].AsString();
            String objectName = request.Inputs["Object Name"].AsString();
            String objClass = request.Inputs["Object Class"].AsString();
            response.WithFiltering().PublishRange(findObjectDN(domainName, objectName, objClass));
        }

        private IEnumerable<ADObject> findObjectDN(String domainName, String objectName, String objClass)
        {
            string connectionPrefix = "LDAP://" + domainName;
            DirectoryEntry entry = new DirectoryEntry(connectionPrefix,credentials.UserName + "@" + credentials.Domain,credentials.Password);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            mySearcher.PageSize = 1000;

            SearchResultCollection resultCollection = objectSearch(domainName, objectName, objClass, mySearcher);
            
            foreach (SearchResult result in resultCollection)
            {
                DirectoryEntry directoryObject = result.GetDirectoryEntry();
                if (directoryObject == null)
                {
                    throw new NullReferenceException("unable to locate the distinguishedName for the object " + objectName + " in the " + domainName + " domain");
                }
                String ldapPath = directoryObject.Path;

                yield return new ADObject(ldapPath);    
                directoryObject.Close();
            }
            entry.Close();
            entry.Dispose();
            mySearcher.Dispose();
        }

        private static SearchResultCollection objectSearch(String domainName, String objectName, String objClass, DirectorySearcher mySearcher)
        {
            mySearcher.PageSize = 1000;
            switch (objClass)
            {
                case "user":
                    mySearcher.Filter = "(&(objectClass=user)(|(cn=" + objectName + ")(sAMAccountName=" + objectName + ")))";
                    break;
                case "group":
                    mySearcher.Filter = "(&(objectClass=group)(|(cn=" + objectName + ")(dn=" + objectName + ")))";
                    break;
                case "computer":
                    mySearcher.Filter = "(&(objectClass=computer)(|(cn=" + objectName + ")(dn=" + objectName + ")))";
                    break;
                case "printqueue":
                    mySearcher.Filter = "(&(objectClass=printQueue)(name=" + objectName + "))";
                    break;
                case "LDAP Search Filter":
                    mySearcher.Filter = objectName;
                    break;
                default:
                    mySearcher.Filter = "(&(objectClass=computer)(|(cn=" + objectName + ")(dn=" + objectName + ")))";
                    break;
            }

            SearchResultCollection resultCollection = mySearcher.FindAll();

            if (resultCollection == null)
            {
                throw new NullReferenceException("unable to locate the distinguishedName for the object " + objectName + " in the " + domainName + " domain");
            }
            return resultCollection;
        }
    }
}

