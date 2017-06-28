using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Set Virtual Machine")]
    class SetVM : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;
        
        private String VM = String.Empty;
        private String Name = String.Empty;
        private String MemoryMB = String.Empty;
        private String NumCpu = String.Empty;
        private String GuestId = String.Empty;
        private String AlternateGuestName = String.Empty;
        private String OSCustomizationSpec = String.Empty;
        private String HARestartPriority = String.Empty;
        private String HAIsolationResponse = String.Empty;
        private String DRSAutomationLevel = String.Empty;
        private String VMSwapfilePolicy = String.Empty;
        private String Description = String.Empty;
        private String Template = String.Empty;
        private String Snapshot = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            string[] GuestIDOptions = new string[72];
            GuestIDOptions[0] = "Asianux Server 3 64 bit";
            GuestIDOptions[1] = "Asianux Server 3";
            GuestIDOptions[2] = "Asianux Server 4 64 bit";
            GuestIDOptions[3] = "Asianux Server 4";
            GuestIDOptions[4] = "Darwin 64 bit";
            GuestIDOptions[5] = "Darwin";
            GuestIDOptions[6] = "Debian GNU/Linux 4 64 bit";
            GuestIDOptions[7] = "Debian GNU/Linux 4";
            GuestIDOptions[8] = "Debian GNU/Linux 5 64 bit";
            GuestIDOptions[9] = "Debian GNU/Linux 5";
            GuestIDOptions[10] = "MS-DOS";
            GuestIDOptions[11] = "FreeBSD x64";
            GuestIDOptions[12] = "FreeBSD";
            GuestIDOptions[13] = "Mandriva Linux";
            GuestIDOptions[14] = "Novell NetWare 4";
            GuestIDOptions[15] = "Novell NetWare 5.1";
            GuestIDOptions[16] = "Novell NetWare 6.x";
            GuestIDOptions[17] = "Novell Linux Desktop 9";
            GuestIDOptions[18] = "Open Enterprise Server";
            GuestIDOptions[19] = "SCO OpenServer 5";
            GuestIDOptions[20] = "SCO OpenServer 6";
            GuestIDOptions[11] = "Linux 2.4x Kernel (64 bit) (experimental)";
            GuestIDOptions[12] = "Linux 2.4x Kernel";
            GuestIDOptions[13] = "Linux 2.6x Kernel (64 bit) (experimental)";
            GuestIDOptions[14] = "Linux 2.6x Kernel";
            GuestIDOptions[15] = "Other Operating System";
            GuestIDOptions[16] = "Other Operating System (64 bit) (experimental)";
            GuestIDOptions[17] = "Linux (64 bit) (experimental)";
            GuestIDOptions[18] = "Other Linux";
            GuestIDOptions[19] = "Red Hat Linux 2.1";
            GuestIDOptions[20] = "Red Hat Enterprise Linux 2";
            GuestIDOptions[21] = "Red Hat Enterprise Linux 3 (64 bit)";
            GuestIDOptions[22] = "Red Hat Enterprise Linux 3";
            GuestIDOptions[23] = "Red Hat Enterprise Linux 4 (64 bit)";
            GuestIDOptions[24] = "Red Hat Enterprise Linux 4";
            GuestIDOptions[25] = "Red Hat Enterprise Linux 5 (64 bit) (experimental)";
            GuestIDOptions[26] = "Red Hat Enterprise Linux 5";
            GuestIDOptions[27] = "Red Hat Enterprise Linux 6 (64 bit)";
            GuestIDOptions[28] = "Red Hat Enterprise Linux 6";
            GuestIDOptions[29] = "Sun Java Desktop System";
            GuestIDOptions[30] = "Suse Linux Enterprise Server 10 (64 bit) (experimental)";
            GuestIDOptions[31] = "Suse Linux Enterprise Server 11 (64 bit)";
            GuestIDOptions[32] = "Suse linux Enterprise Server 11";
            GuestIDOptions[33] = "Suse Linux Enterprise Server 9 (64 bit)";
            GuestIDOptions[34] = "Suse Linux Enterprise Server 9";
            GuestIDOptions[35] = "Solaris 10 (64 bit) (experimental)";
            GuestIDOptions[36] = "Solaris 10 (32 bit) (experimental)";
            GuestIDOptions[37] = "Solaris 6";
            GuestIDOptions[38] = "Solaris 7";
            GuestIDOptions[39] = "Solaris 8";
            GuestIDOptions[30] = "Suse Linux (64 bit)";
            GuestIDOptions[40] = "Suse Linux";
            GuestIDOptions[41] = "Turbolinux (64 bit)";
            GuestIDOptions[42] = "Turbolinux";
            GuestIDOptions[43] = "Ubuntu Linux (64 bit)";
            GuestIDOptions[44] = "Ubuntu Linux";
            GuestIDOptions[45] = "SCO UnixWare 7";
            GuestIDOptions[46] = "Windows 2000 Advanced Server";
            GuestIDOptions[47] = "Windows 2000 Professional";
            GuestIDOptions[48] = "Windows 2000 Server";
            GuestIDOptions[49] = "Windows 3.1";
            GuestIDOptions[50] = "Windows 95";
            GuestIDOptions[51] = "Windows 98";
            GuestIDOptions[52] = "Windows 7 (64 bit)";
            GuestIDOptions[53] = "Windows 7";
            GuestIDOptions[54] = "Windows Server 2008 R2 (64 bit)";
            GuestIDOptions[55] = "Windows Longhorn (64 bit) (experimental)";
            GuestIDOptions[56] = "Windows Longhorn (experimental)";
            GuestIDOptions[57] = "Windows Millenium Edition";
            GuestIDOptions[58] = "Windows Small Business Server 2003";
            GuestIDOptions[59] = "Windows Server 2003, Datacenter Edition (64 bit) (experimental)";
            GuestIDOptions[60] = "Windows Server 2003, Datacenter Edition";
            GuestIDOptions[61] = "Windows Server 2003, Enterprise Edition (64 bit)";
            GuestIDOptions[62] = "Windows Server 2003, Enterprise Edition";
            GuestIDOptions[63] = "Windows Server 2003, Standard Edition (64 bit)";
            GuestIDOptions[64] = "Windows Server 2003, Standard Edition";
            GuestIDOptions[65] = "Windows Server 2003, Web Edition";
            GuestIDOptions[66] = "Windows NT 4";
            GuestIDOptions[67] = "Windows Vista (64 bit)";
            GuestIDOptions[68] = "Windows Vista";
            GuestIDOptions[69] = "Windows XP Home Edition";
            GuestIDOptions[70] = "Windows XP Professional Edition (64 bit)";
            GuestIDOptions[71] = "Windows XP Professional";

            string[] HARestartPriorityOptions = new string[5];
            HARestartPriorityOptions[0] = "Disabled";
            HARestartPriorityOptions[1] = "Low";
            HARestartPriorityOptions[2] = "Medium";
            HARestartPriorityOptions[3] = "High";
            HARestartPriorityOptions[4] = "ClusterRestartPriority";

            string[] HAIsolationResponseOptions = new string[3];
            HAIsolationResponseOptions[0] = "AsSpecifiedByCluster";
            HAIsolationResponseOptions[1] = "PowerOff";
            HAIsolationResponseOptions[2] = "DoNothing";

            string[] DrsAutomationLevelOptions = new string[3];
            DrsAutomationLevelOptions[0] = "FullyAutomated";
            DrsAutomationLevelOptions[1] = "Manual";
            DrsAutomationLevelOptions[2] = "PartiallyAutomated";

            string[] VMSwapFilePolicyOptions = new string[2];
            VMSwapFilePolicyOptions[0] = "InHostDataStore";
            VMSwapFilePolicyOptions[1] = "WithVM";

            string[] NumCPUOptions = new string[8];
            NumCPUOptions[0] = "1";
            NumCPUOptions[1] = "2";
            NumCPUOptions[2] = "3";
            NumCPUOptions[3] = "4";
            NumCPUOptions[4] = "5";
            NumCPUOptions[5] = "6";
            NumCPUOptions[6] = "7";
            NumCPUOptions[7] = "8";

            string[] toTemplateOptions = new string[2];
            toTemplateOptions[0] = "true";
            toTemplateOptions[1] = "false";

            designer.AddInput("VM");
            designer.AddInput("Name").NotRequired();
            designer.AddInput("MemoryMB").NotRequired().WithDefaultValue("4096");
            designer.AddInput("NumCpu").NotRequired().WithListBrowser(NumCPUOptions);
            designer.AddInput("GuestId").NotRequired().WithListBrowser(GuestIDOptions);
            designer.AddInput("AlternateGuestName").NotRequired();
            designer.AddInput("OSCustomizationSpec").NotRequired();
            designer.AddInput("HARestartPriority").NotRequired().WithListBrowser(HARestartPriorityOptions); 
            designer.AddInput("HAIsolationResponse").NotRequired().WithListBrowser(HAIsolationResponseOptions);
            designer.AddInput("DRSAutomationLevel").NotRequired().WithListBrowser(DrsAutomationLevelOptions);
            designer.AddInput("VMSwapfilePolicy").NotRequired().WithListBrowser(VMSwapFilePolicyOptions);
            designer.AddInput("Description").NotRequired();
            designer.AddInput("Convert To Template").NotRequired().WithListBrowser(toTemplateOptions);
            designer.AddInput("Snapshot").NotRequired();

            designer.AddCorellatedData(typeof(vm));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

                    
            VM = request.Inputs["VM"].AsString();
            Name = request.Inputs["Name"].AsString();
            MemoryMB = request.Inputs["MemoryMB"].AsString();
            NumCpu = request.Inputs["NumCpu"].AsString();
            GuestId = request.Inputs["GuestId"].AsString();
            AlternateGuestName = request.Inputs["AlternateGuestName"].AsString();
            OSCustomizationSpec = request.Inputs["OSCustomizationSpec"].AsString();
            HARestartPriority = request.Inputs["HARestartPriority"].AsString();
            HAIsolationResponse = request.Inputs["HAIsolationResponse"].AsString();
            DRSAutomationLevel = request.Inputs["DRSAutomationLevel"].AsString();
            VMSwapfilePolicy = request.Inputs["VMSwapfilePolicy"].AsString();
            Description = request.Inputs["Description"].AsString();
            Template = request.Inputs["Template"].AsString();
            Snapshot = request.Inputs["Snapshot"].AsString();

            response.WithFiltering().PublishRange(getVM());
        }

        private IEnumerable<vm> getVM()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";
            String preCommand = "";
            String command = "Set-VM";

            if (!(VM == String.Empty)) 
            {
                preCommand += "$VM = Get-VM -Name \"" + VM + "\"\n";
                command += " -VM $VM"; 
            }
            if (!(Name == String.Empty)) { command += " -Name \"" + Name + "\""; }
            if (!(MemoryMB == String.Empty)) { command += " -MemoryMB \"" + MemoryMB + "\""; }
            if (!(NumCpu == String.Empty)) { command += " -NumCpu \"" + NumCpu + "\""; }
            if (!(GuestId == String.Empty))
            {
                switch (GuestId)
                {
                    case "Asianux Server 3 64 bit":
                        command += " -GuestId asianux3_64Guest";
                        break;
                    case "Asianux Server 3":
                        command += " -GuestId asianux3Guest";
                        break;
                    case "Asianux Server 4 64 bit":
                        command += " -GuestId asianux4_64Guest";
                        break;
                    case "Asianux Server 4":
                        command += " -GuestId asianux4Guest";
                        break;
                    case "Darwin 64 bit":
                        command += " -GuestId darwin64Guest";
                        break;
                    case "Darwin":
                        command += " -GuestId darwinGuest";
                        break;
                    case "Debian GNU/Linux 4 64 bit":
                        command += " -GuestId debian4_64Guest";
                        break;
                    case "Debian GNU/Linux 4":
                        command += " -GuestId debian4Guest";
                        break;
                    case "Debian GNU/Linux 5 64 bit":
                        command += " -GuestId debian5_64Guest";
                        break;
                    case "Debian GNU/Linux 5":
                        command += " -GuestId debian5Guest";
                        break;
                    case "MS-DOS":
                        command += " -GuestId dosGuest";
                        break;
                    case "FreeBSD x64":
                        command += " -GuestId freebsd64Guest";
                        break;
                    case "FreeBSD":
                        command += " -GuestId freebsdGuest";
                        break;
                    case "Mandriva Linux":
                        command += " -GuestId mandrivaGuest";
                        break;
                    case "Novell NetWare 4":
                        command += " -GuestId netware4Guest";
                        break;
                    case "Novell NetWare 5.1":
                        command += " -GuestId netware5Guest";
                        break;
                    case "Novell NetWare 6.x":
                        command += " -GuestId netware6Guest";
                        break;
                    case "Novell Linux Desktop 9":
                        command += " -GuestId nld9Guest";
                        break;
                    case "Open Enterprise Server":
                        command += " -GuestId oesGuest";
                        break;
                    case "SCO OpenServer 5":
                        command += " -GuestId openServer5Guest";
                        break;
                    case "SCO OpenServer 6":
                        command += " -GuestId openServer6Guest";
                        break;
                    case "OS/2":
                        command += " -GuestId os2Guest";
                        break;
                    case "Linux 2.4x Kernel (64 bit) (experimental)":
                        command += " -GuestId other24xLinux64Guest";
                        break;
                    case "Linux 2.4x Kernel":
                        command += " -GuestId other24xLinuxGuest";
                        break;
                    case "Linux 2.6x Kernel (64 bit) (experimental)":
                        command += " -GuestId other26xLinux64Guest";
                        break;
                    case "Linux 2.6x Kernel":
                        command += " -GuestId other26xLinuxGuest";
                        break;
                    case "Other Operating System":
                        command += " -GuestId otherGuest";
                        break;
                    case "Other Operating System (64 bit) (experimental)":
                        command += " -GuestId otherGuest64";
                        break;
                    case "Linux (64 bit) (experimental)":
                        command += " -GuestId otherLinux64Guest ";
                        break;
                    case "Other Linux":
                        command += " -GuestId otherLinuxGuest";
                        break;
                    case "Red Hat Linux 2.1":
                        command += " -GuestId redhatGuest";
                        break;
                    case "Red Hat Enterprise Linux 2":
                        command += " -GuestId rhel2Guest";
                        break;
                    case "Red Hat Enterprise Linux 3 (64 bit)":
                        command += " -GuestId rhel3_64Guest";
                        break;
                    case "Red Hat Enterprise Linux 3":
                        command += " -GuestId rhel3Guest";
                        break;
                    case "Red Hat Enterprise Linux 4 (64 bit)":
                        command += " -GuestId rhel4_64Guest";
                        break;
                    case "Red Hat Enterprise Linux 4":
                        command += " -GuestId rhel4Guest";
                        break;
                    case "Red Hat Enterprise Linux 5 (64 bit) (experimental)":
                        command += " -GuestId rhel5_64Guest";
                        break;
                    case "Red Hat Enterprise Linux 5":
                        command += " -GuestId rhel5Guest";
                        break;
                    case "Red Hat Enterprise Linux 6 (64 bit)":
                        command += " -GuestId rhel6_64Guest";
                        break;
                    case "Red Hat Enterprise Linux 6":
                        command += " -GuestId rhel6Guest";
                        break;
                    case "Sun Java Desktop System":
                        command += " -GuestId sjdsGuest";
                        break;
                    case "Suse Linux Enterprise Server 10 (64 bit) (experimental)":
                        command += " -GuestId sles10_64Guest";
                        break;
                    case "Suse linux Enterprise Server 10":
                        command += " -GuestId sles10Guest";
                        break;
                    case "Suse Linux Enterprise Server 11 (64 bit)":
                        command += " -GuestId sles11_64Guest";
                        break;
                    case "Suse linux Enterprise Server 11":
                        command += " -GuestId sles11Guest";
                        break;
                    case "Suse Linux Enterprise Server 9 (64 bit)":
                        command += " -GuestId sles64Guest";
                        break;
                    case "Suse Linux Enterprise Server 9":
                        command += " -GuestId slesGuest";
                        break;
                    case "Solaris 10 (64 bit) (experimental)":
                        command += " -GuestId solaris10_64Guest";
                        break;
                    case "Solaris 10 (32 bit) (experimental)":
                        command += " -GuestId solaris10Guest";
                        break;
                    case "Solaris 6":
                        command += " -GuestId solaris6Guest";
                        break;
                    case "Solaris 7":
                        command += " -GuestId solaris7Guest";
                        break;
                    case "Solaris 8":
                        command += " -GuestId solaris8Guest";
                        break;
                    case "solaris 9":
                        command += " -GuestId solaris9Guest";
                        break;
                    case "Suse Linux (64 bit)":
                        command += " -GuestId suse64Guest";
                        break;
                    case "Suse Linux":
                        command += " -GuestId suseGuest";
                        break;
                    case "Turbolinux (64 bit)":
                        command += " -GuestId turboLinux64Guest";
                        break;
                    case "Turbolinux":
                        command += " -GuestId turboLinuxGuest";
                        break;
                    case "Ubuntu Linux (64 bit)":
                        command += " -GuestId ubuntu64Guest";
                        break;
                    case "Ubuntu Linux":
                        command += " -GuestId ubuntuGuest";
                        break;
                    case "SCO UnixWare 7":
                        command += " -GuestId unixWare7Guest";
                        break;
                    case "Windows 2000 Advanced Server":
                        command += " -GuestId win2000AdvServGuest";
                        break;
                    case "Windows 2000 Professional":
                        command += " -GuestId win2000ProGuest";
                        break;
                    case "Windows 2000 Server":
                        command += " -GuestId win2000ServGuest";
                        break;
                    case "Windows 3.1":
                        command += " -GuestId win31Guest";
                        break;
                    case "Windows 95":
                        command += " -GuestId win95Guest";
                        break;
                    case "Windows 98":
                        command += " -GuestId win98Guest";
                        break;
                    case "Windows 7 (64 bit)":
                        command += " -GuestId windows7_64Guest";
                        break;
                    case "Windows 7":
                        command += " -GuestId windows7Guest";
                        break;
                    case "Windows Server 2008 R2 (64 bit)":
                        command += " -GuestId windows7Server64Guest";
                        break;
                    case "Windows Longhorn (64 bit) (experimental)":
                        command += " -GuestId winLonghorn64Guest";
                        break;
                    case "Windows Longhorn (experimental)":
                        command += " -GuestId winLonghornGuest";
                        break;
                    case "Windows Millenium Edition":
                        command += " -GuestId winMeGuest";
                        break;
                    case "Windows Small Business Server 2003":
                        command += " -GuestId winNetBusinessGuest";
                        break;
                    case "Windows Server 2003, Datacenter Edition (64 bit) (experimental)":
                        command += " -GuestId winNetDatacenter64Guest";
                        break;
                    case "Windows Server 2003, Datacenter Edition":
                        command += " -GuestId winNetDatacenterGuest";
                        break;
                    case "Windows Server 2003, Enterprise Edition (64 bit)":
                        command += " -GuestId winNetEnterprise64Guest";
                        break;
                    case "Windows Server 2003, Enterprise Edition":
                        command += " -GuestId winNetEnterpriseGuest";
                        break;
                    case "Windows Server 2003, Standard Edition (64 bit)":
                        command += " -GuestId winNetStandard64Guest";
                        break;
                    case "Windows Server 2003, Standard Edition":
                        command += " -GuestId winNetStandardGuest";
                        break;
                    case "Windows Server 2003, Web Edition":
                        command += " -GuestId winNetWebGuest";
                        break;
                    case "Windows NT 4":
                        command += " -GuestId winNTGuest";
                        break;
                    case "Windows Vista (64 bit)":
                        command += " -GuestId winVista64Guest";
                        break;
                    case "Windows Vista":
                        command += " -GuestId winVistaGuest";
                        break;
                    case "Windows XP Home Edition":
                        command += " -GuestId winXPHomeGuest";
                        break;
                    case "Windows XP Professional Edition (64 bit)":
                        command += " -GuestId winXPPro64Guest";
                        break;
                    case "Windows XP Professional":
                        command += " -GuestId winXPProGuest";
                        break;
                }
            }
            if (!(AlternateGuestName == String.Empty)) { command += " -AlternateGuestName \"" + AlternateGuestName + "\""; }
            if (!(OSCustomizationSpec == String.Empty)) { command += " -OSCustomizationSpec \"" + OSCustomizationSpec + "\""; }
            if (!(HARestartPriority == String.Empty)) { command += " -HARestartPriority \"" + HARestartPriority + "\""; }
            if (!(HAIsolationResponse == String.Empty)) { command += " -HAIsolationResponse \"" + HAIsolationResponse + "\""; }
            if (!(DRSAutomationLevel == String.Empty)) { command += " -DRSAutomationLevel \"" + DRSAutomationLevel + "\""; }
            if (!(VMSwapfilePolicy == String.Empty)) { command += " -VMSwapfilePolicy \"" + VMSwapfilePolicy + "\""; }
            if (!(Description == String.Empty)) { command += " -Description \"" + Description + "\""; }
            if (Template.Equals("true")) { command += " -ToTemplate"; }
            if (!(Snapshot == String.Empty)) 
            {
                preCommand += "$Snapshot = Get-Snapshot -VM $VM -Name \"" + Snapshot + "\"\n";
                command += " -Snapshot $Snapshot";
            }

            

            Script += command + " -Confirm:$False";

            pipeline.Commands.AddScript(Script);

            Collection<PSObject> results = new Collection<PSObject>();

            try
            {
                results = pipeline.Invoke();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            foreach (PSObject obj in results)
            {
                if (obj.BaseObject.GetType().ToString().Contains("VMware.VimAutomation.ViCore.Impl.V1.Inventory.VirtualMachineImpl"))
                {
                    String PowerState = obj.Members["PowerState"].Value.ToString();
                    String VMVersion = obj.Members["Version"].Value.ToString();
                    String Number_Cpu = obj.Members["NumCpu"].Value.ToString();
                    String Memory_MB = obj.Members["MemoryMB"].Value.ToString();
                    String HostId = obj.Members["HostId"].Value.ToString();
                    String FolderId = obj.Members["FolderId"].Value.ToString();
                    String ResourcePoolId = obj.Members["ResourcePoolId"].Value.ToString();
                    String UsedSpaceGB = obj.Members["UsedSpaceGB"].Value.ToString();
                    String ProvisionedSpaceGB = obj.Members["ProvisionedSpaceGB"].Value.ToString();
                    String id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    String VM_Description = String.Empty;
                    String Notes = String.Empty;
                    try { VM_Description = obj.Members["Description"].Value.ToString(); }
                    catch { }
                    try { Notes = obj.Members["Notes"].Value.ToString(); }
                    catch { }

                    yield return new vm(PowerState, VMVersion, VM_Description, Notes, Number_Cpu, Memory_MB, HostId, FolderId, ResourcePoolId, UsedSpaceGB, ProvisionedSpaceGB, id, name);
                }
            }
            runspace.Close();
        }
    }
}

