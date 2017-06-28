using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("New OS Customization Spec")]
    class NewOSCustomizationSpec : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String OSType = String.Empty;
        private String Name = String.Empty;
        private String Type = String.Empty;
        private String DnsServer = String.Empty;
        private String DnsSuffix = String.Empty;
        private String Domain = String.Empty;
        private String NamingScheme = String.Empty;
        private String NamingPrefix = String.Empty;
        private String Description = String.Empty;
        private String FullName = String.Empty;
        private String OrgName = String.Empty;
        private String ChangeSid = String.Empty;
        private String DeleteAccounts = String.Empty;
        private String GuiRunOnce = String.Empty;
        private String AdminPassword = String.Empty;
        private String TimeZone = String.Empty;
        private String AutoLogonCount = String.Empty;
        private String Workgroup = String.Empty;
        private String DomainUsername = String.Empty;
        private String DomainPassword = String.Empty;
        private String Product = String.Empty;
        private String LicenseMode = String.Empty;
        private String LicenseMaxConnections = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            string[] OSTypeOptions = new string[2];
            OSTypeOptions[0] = "Windows";
            OSTypeOptions[1] = "Linux";
            
            string[] TypeOptions = new string[2];
            TypeOptions[0] = "Persistent";
            TypeOptions[1] = "NonPersistent";

            string[] NamingSchemeOptions = new string[4];
            NamingSchemeOptions[0] = "Custom";
            NamingSchemeOptions[1] = "Fixed";
            NamingSchemeOptions[2] = "Prefix";
            NamingSchemeOptions[3] = "Vm";

            string[] ChangeSidOptions = new string[2];
            ChangeSidOptions[0] = "true";
            ChangeSidOptions[1] = "false";

            string[] DeleteAccountsOptions = new string[2];
            ChangeSidOptions[0] = "true";
            ChangeSidOptions[1] = "false";

            string[] TimeZoneOptions = new string[76];
            TimeZoneOptions[0] = "000 Int'l Dateline";
            TimeZoneOptions[1] = "001 Samoa";
            TimeZoneOptions[2] = "002 Hawaii";
            TimeZoneOptions[4] = "003 Alaskan";
            TimeZoneOptions[5] = "004 Pacific";
            TimeZoneOptions[6] = "010 Mountain (U.S. and Canada)";
            TimeZoneOptions[7] = "015 U.S. Mountain: Arizona";
            TimeZoneOptions[8] = "020 Central (U.S. and Canada)";
            TimeZoneOptions[9] = "025 Canada Central";
            TimeZoneOptions[10] = "030 Mexico";
            TimeZoneOptions[11] = "033 Central America";
            TimeZoneOptions[12] = "035 Eastern (U.S. and Canada)";
            TimeZoneOptions[13] = "040 U.S. Eastern: Indiana (East)";
            TimeZoneOptions[14] = "045 S.A. Pacific";
            TimeZoneOptions[15] = "050 Atlantic (Canada)";
            TimeZoneOptions[16] = "055 S.A. Western";
            TimeZoneOptions[17] = "056 Pacific S.A.";
            TimeZoneOptions[18] = "060 Newfoundland";
            TimeZoneOptions[19] = "065 E. South America";
            TimeZoneOptions[20] = "070 S.A. Eastern";
            TimeZoneOptions[21] = "073 Greenland";
            TimeZoneOptions[22] = "075 Mid-Atlantic";
            TimeZoneOptions[23] = "080 Azores";
            TimeZoneOptions[24] = "083 Cape Verde Islands";
            TimeZoneOptions[25] = "085 GMT (Greenwich Mean Time)";
            TimeZoneOptions[26] = "085 GMT (Greenwich Mean Time)";
            TimeZoneOptions[27] = "090 GMT Greenwich";
            TimeZoneOptions[28] = "095 Central Europe";
            TimeZoneOptions[29] = "100 Central European";
            TimeZoneOptions[30] = "105 Romance";
            TimeZoneOptions[31] = "110 W. Europe";
            TimeZoneOptions[32] = "113 W. Central Africa";
            TimeZoneOptions[33] = "115 E. Europe";
            TimeZoneOptions[34] = "120 Egypt";
            TimeZoneOptions[35] = "125 EET (Helsinki, Riga, Tallinn)";
            TimeZoneOptions[36] = "130 EET (Athens, Istanbul, Minsk)";
            TimeZoneOptions[37] = "135 Israel: Jerusalem";
            TimeZoneOptions[38] = "140 S. Africa: Harare, Pretoria";
            TimeZoneOptions[39] = "145 Russian";
            TimeZoneOptions[40] = "150 Arab";
            TimeZoneOptions[41] = "155 E. Africa";
            TimeZoneOptions[42] = "160 Iran";
            TimeZoneOptions[43] = "165 Arabian";
            TimeZoneOptions[44] = "170 Caucasus Pacific (U.S. and Canada)";
            TimeZoneOptions[45] = "175 Afghanistan";
            TimeZoneOptions[46] = "180 Russia Yekaterinburg";
            TimeZoneOptions[47] = "185 W. Asia";
            TimeZoneOptions[48] = "190 India";
            TimeZoneOptions[49] = "193 Nepal";
            TimeZoneOptions[50] = "195 Central Asia";
            TimeZoneOptions[51] = "200 Sri Lanka";
            TimeZoneOptions[52] = "201 N. Central Asia";
            TimeZoneOptions[53] = "203 Myanmar: Rangoon";
            TimeZoneOptions[54] = "205 S.E. Asia";
            TimeZoneOptions[55] = "207 N. Asia";
            TimeZoneOptions[56] = "210 China";
            TimeZoneOptions[57] = "215 Singapore";
            TimeZoneOptions[58] = "215 Singapore";
            TimeZoneOptions[59] = "220 Taipei";
            TimeZoneOptions[60] = "225 W. Australia";
            TimeZoneOptions[61] = "227 N. Asia East";
            TimeZoneOptions[62] = "230 Korea: Seoul";
            TimeZoneOptions[63] = "235 Tokyo";
            TimeZoneOptions[64] = "240 Sakha Yakutsk";
            TimeZoneOptions[65] = "245 A.U.S. Central: Darwin";
            TimeZoneOptions[66] = "250 Central Australia";
            TimeZoneOptions[67] = "255 A.U.S. Eastern";
            TimeZoneOptions[68] = "260 E. Australia";
            TimeZoneOptions[69] = "265 Tasmania";
            TimeZoneOptions[70] = "270 Vladivostok";
            TimeZoneOptions[71] = "275 W. Pacific";
            TimeZoneOptions[72] = "280 Central Pacific";
            TimeZoneOptions[73] = "285 Fiji";
            TimeZoneOptions[74] = "290 New Zealand";
            TimeZoneOptions[75] = "300 Tonga";

            string[] LicenseModeOptions = new string[3];
            LicenseModeOptions[0] = "Perseat";
            LicenseModeOptions[1] = "Perserver";
            LicenseModeOptions[2] = "Notspecified";
            
            designer.AddInput("OSType").NotRequired().WithListBrowser(OSTypeOptions);
            designer.AddInput("Name");
            designer.AddInput("Type").NotRequired().WithListBrowser(TypeOptions);
            designer.AddInput("DnsServer").NotRequired();
            designer.AddInput("DnsSuffix").NotRequired();
            designer.AddInput("Domain").NotRequired();
            designer.AddInput("NamingScheme").NotRequired().WithListBrowser(NamingSchemeOptions);
            designer.AddInput("NamingPrefix").NotRequired();
            designer.AddInput("Description").NotRequired();
            designer.AddInput("NamingPrefix").NotRequired();
            designer.AddInput("FullName").NotRequired();
            designer.AddInput("OrgName").NotRequired();
            designer.AddInput("ChangeSid").NotRequired();
            designer.AddInput("DeleteAccounts").NotRequired().WithListBrowser(DeleteAccountsOptions);
            designer.AddInput("GuiRunOnce").NotRequired();
            designer.AddInput("AdminPassword").NotRequired().PasswordProtect();
            designer.AddInput("TimeZone").NotRequired().WithListBrowser(TimeZoneOptions);
            designer.AddInput("AutoLogonCount").NotRequired();
            designer.AddInput("Workgroup").NotRequired();
            designer.AddInput("AutoLogonCount").NotRequired();
            designer.AddInput("DomainUsername").NotRequired();
            designer.AddInput("DomainPassword").NotRequired().PasswordProtect();
            designer.AddInput("ProductKey").NotRequired();
            designer.AddInput("LicenseMode").NotRequired().WithListBrowser(LicenseModeOptions);
            designer.AddInput("LicenseMaxConnections").NotRequired();
            
            designer.AddOutput("Customization Spec Name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            OSType = request.Inputs["OSType"].AsString();
            Name = request.Inputs["Name"].AsString();
            Type = request.Inputs["Type"].AsString();
            DnsServer = request.Inputs["DnsServer"].AsString();
            DnsSuffix = request.Inputs["DnsSuffix"].AsString();
            Domain = request.Inputs["Domain"].AsString();
            NamingScheme = request.Inputs["NamingScheme"].AsString();
            NamingPrefix = request.Inputs["NamingPrefix"].AsString();
            Description = request.Inputs["Description"].AsString();
            FullName = request.Inputs["FullName"].AsString();
            OrgName = request.Inputs["OrgName"].AsString();
            ChangeSid = request.Inputs["ChangeSid"].AsString();
            DeleteAccounts = request.Inputs["DeleteAccounts"].AsString();
            GuiRunOnce = request.Inputs["GuiRunOnce"].AsString();
            AdminPassword = request.Inputs["AdminPassword"].AsString();
            TimeZone = request.Inputs["TimeZone"].AsString();
            AutoLogonCount = request.Inputs["AutoLogonCount"].AsString();
            Workgroup = request.Inputs["Workgroup"].AsString();
            DomainUsername = request.Inputs["DomainUsername"].AsString();
            DomainPassword = request.Inputs["DomainPassword"].AsString();
            Product = request.Inputs["Product"].AsString();
            LicenseMode = request.Inputs["LicenseMode"].AsString();
            LicenseMaxConnections = request.Inputs["LicenseMaxConnections"].AsString();

            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "New-OSCustomizationSpec";

            if (!(OSType == String.Empty)) { command += " -OSType \"" + OSType + "\""; }
            if (!(Name == String.Empty)) { command += " -Name \"" + Name + "\""; }
            if (!(DnsServer == String.Empty)) { command += " -DnsServer \"" + DnsServer + "\""; }
            if (!(DnsSuffix == String.Empty)) { command += " -DnsSuffix \"" + DnsSuffix + "\""; }
            if (!(Domain == String.Empty)) { command += " -Domain \"" + Domain + "\""; }
            if (!(NamingScheme == String.Empty)) { command += " -NamingScheme \"" + NamingScheme + "\""; }
            if (!(Description == String.Empty)) { command += " -Description \"" + Description + "\""; }
            if (!(NamingPrefix == String.Empty)) { command += " -NamingPrefix \"" + NamingPrefix + "\""; }
            if (!(FullName == String.Empty)) { command += " -FullName \"" + FullName + "\""; }
            if (!(OrgName == String.Empty)) { command += " -OrgName \"" + OrgName + "\""; }
            if (!(ChangeSid.Equals("true"))) { command += " -ChangeSid:$True"; }
            else { command += " -ChangeSid:$False"; }
            if (!(DeleteAccounts == String.Empty)) { command += " -DeleteAccounts \"" + DeleteAccounts + "\""; }
            if (!(GuiRunOnce == String.Empty)) { command += " -GuiRunOnce \"" + GuiRunOnce + "\""; }
            if (!(AdminPassword == String.Empty)) { command += " -AdminPassword \"" + AdminPassword + "\""; }
            if (!(TimeZone == String.Empty)) { command += " -TimeZone \"" + TimeZone + "\""; }
            if (!(AutoLogonCount == String.Empty)) { command += " -AutoLogonCount \"" + AutoLogonCount + "\""; }
            if (!(Workgroup == String.Empty)) { command += " -Workgroup \"" + Workgroup + "\""; }
            if (!(DomainUsername == String.Empty)) { command += " -DomainUsername \"" + DomainUsername + "\""; }
            if (!(DomainPassword == String.Empty)) { command += " -DomainPassword \"" + DomainPassword + "\""; }
            if (!(Product == String.Empty)) { command += " -Product \"" + Product + "\""; }
            if (!(LicenseMode == String.Empty)) { command += " -LicenseMode \"" + LicenseMode + "\""; }
            if (!(LicenseMaxConnections == String.Empty)) { command += " -LicenseMaxConnections \"" + LicenseMaxConnections + "\""; }

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

            runspace.Close();

            response.Publish("Customization Spec Name", Name);
        }
    }
}

