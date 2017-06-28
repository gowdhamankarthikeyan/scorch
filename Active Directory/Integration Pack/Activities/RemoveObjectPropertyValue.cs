using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Remove Multi-Value Object Property Value")]
    class RemoveObjectPropertyValue : IActivity
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
            designer.AddInput("Object LDAP Path").WithDefaultValue("LDAP://Contoso.com/cn=TEST,ou=Container,DC=Contoso,DC=Com");
            designer.AddInput("Property Name");
            designer.AddInput("Property Value");
            designer.AddCorellatedData(typeof(ObjectValues));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string LDAPPath = CapitalizeLDAPPath(request.Inputs["Object LDAP Path"].AsString());
            string propertyName = request.Inputs["Property Name"].AsString();
            string propertyValue = request.Inputs["Property Value"].AsString();
            response.WithFiltering().PublishRange(removeObjectPropertyValue(LDAPPath, propertyName, propertyValue));
        }

        private IEnumerable<ObjectValues> removeObjectPropertyValue(string LDAPPath, string propertyName, string propertyValue)
        {
            DirectoryEntry objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            if (objRootDSE.Properties.Contains(propertyName))
            {
                if (objRootDSE.Properties[propertyName].Contains(propertyValue))
                {
                    objRootDSE.Properties[propertyName].Remove(propertyValue);
                    objRootDSE.CommitChanges();
                    objRootDSE.Close();

                    objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
                    if (objRootDSE.Properties.Contains(propertyName))
                    {
                        //Multi-Value Property
                        if (objRootDSE.Properties[propertyName].Count > 1)
                        {
                            PropertyValueCollection ValueCollection = objRootDSE.Properties[propertyName];
                            IEnumerator en = ValueCollection.GetEnumerator();

                            while (en.MoveNext())
                            {
                                if (en.Current != null)
                                {
                                    yield return new ObjectValues(LDAPPath, propertyName, en.Current.ToString());
                                }
                            }
                        }
                        //Single Value Property
                        else
                        {
                            yield return new ObjectValues(LDAPPath, propertyName, objRootDSE.Properties[propertyName].Value.ToString());
                        }
                    }
                    else
                    {
                        yield return new ObjectValues(LDAPPath, propertyName, "NULL");
                    }
                }
                else
                {
                    throw new Exception("Current Object " + LDAPPath + " does not contain any instance of Property " + propertyName + " with value " + propertyValue);
                }
            }
            else
            {
                throw new Exception("Current Object " + LDAPPath + " does not contain any instance of Property " + propertyName);
            }
        }

        private string CapitalizeLDAPPath(string value)
        {
            if (value == null)
                throw new Exception("Must enter valid LDAP Path");
            if (!value.StartsWith("LDAP", true, System.Globalization.CultureInfo.CurrentCulture))
                throw new Exception("Must enter valid LDAP Path");

            StringBuilder result = new StringBuilder(value);
            for (int i = 0; i < 4; i++)
            {
                result[i] = char.ToUpper(result[i]);
            }
            return result.ToString();
        }
    }
}

