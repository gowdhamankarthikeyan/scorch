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
    public class Activity
    {
        public Activity()
        {
        }

        private string _name;
        private string _id;
        private string _description;
        private string _details;
        private string _bitmap16;
        private string _bitmap32;
        private string _bitmapMask;
        private bool _isMonitor;
        private bool _showPropertiesPage;
        private bool _showFiltersPage;
        private bool _showAlternateDisplayPage;

    }
}
