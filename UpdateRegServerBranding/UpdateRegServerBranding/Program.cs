using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EnterpriseManagement;
using Microsoft.EnterpriseManagement.Configuration;
using Microsoft.EnterpriseManagement.Common;


namespace UpdateRegServerBranding
{
    class Program
    {
        static void Main(string[] args)
        {
            //string netbiosComputerName = System.Environment.MachineName;
            string netbiosComputerName = "mgoscomtd1";
            string attestationFrequency = string.Empty;
            string ChargebackGroup = string.Empty;
            string Description = string.Empty;
            string NotificationGroup = string.Empty;
            string ServerOwner1 = string.Empty;
            string ServiceLevel = string.Empty;
            string VirtualCenter = string.Empty;

            getServiceManagerBrandingProperties(args[0], netbiosComputerName, ref attestationFrequency, ref ChargebackGroup, 
                                                ref Description, ref NotificationGroup, ref ServerOwner1, ref ServiceLevel, 
                                                ref VirtualCenter);


        }

        private static void getServiceManagerBrandingProperties(string managementServerName, string netbiosComputerName, ref string attestationFrequency,
                                                                ref string ChargebackGroup, ref string Description, ref string NotificationGroup, 
                                                                ref string ServerOwner1, ref string ServiceLevel, ref string VirtualCenter)
        {
            //Connect to Management Group
            EnterpriseManagementGroup mg = new EnterpriseManagementGroup(managementServerName);
            ManagementPack mp = mg.GetManagementPack("GMI.SM.BaseClasses.Extension", "c17ba471fb087385", new Version());

            ManagementPackClass requestClass = mp.GetClass("ClassExtension_c270fe5b_740f_4a9c_828c_b35e8fe739a9");
            
            EnterpriseManagementObjectCriteria criteria = new EnterpriseManagementObjectCriteria(String.Format("NetbiosComputerName='{0}'", netbiosComputerName), requestClass);
            IObjectReader<EnterpriseManagementObject> reader = mg.EntityObjects.GetObjectReader<EnterpriseManagementObject>(criteria, ObjectQueryOptions.Default);

            foreach (EnterpriseManagementObject item in reader)
            {
                foreach(ManagementPackProperty mpp in item.GetProperties())
                {
                    switch(mpp.Name)
                    {
                        case "AttestationFrequency":
                            try { attestationFrequency = item[mpp].Value.ToString(); }
                            catch { ChargebackGroup = String.Empty; }
                            break;
                        case "ChargebackGroup":
                            try { ChargebackGroup = item[mpp].Value.ToString(); }
                            catch { ChargebackGroup = String.Empty; }
                            break;
                        case "Description":
                            try { Description = item[mpp].Value.ToString(); }
                            catch { Description = String.Empty; } 
                            break;
                        case "NotificationGroup":
                            try { NotificationGroup = item[mpp].Value.ToString(); }
                            catch { ChargebackGroup = String.Empty; }
                            break;
                        case "ServerOwner1":
                            try { ServerOwner1 = item[mpp].Value.ToString(); }
                            catch { ChargebackGroup = String.Empty; }
                            break;
                        case "ServiceLevel":
                            try { ServiceLevel = item[mpp].Value.ToString(); }
                            catch { ChargebackGroup = String.Empty; }
                            break;
                        case "VirtualCenter":
                            try { VirtualCenter = item[mpp].Value.ToString(); }
                            catch { ChargebackGroup = String.Empty; }
                            break;
                    }
                }
            }
        }
    }
}
