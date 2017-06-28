using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using System.Xml;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.IntegrationPack
{
    public class Configuration
    {
        public Configuration()
        {
        }

        private string _name;
        private Guid _id;
        private string _menuLabel;
        private string _menuDescription;
        private string _verb;
        private string _extraData;


       
    }
}
