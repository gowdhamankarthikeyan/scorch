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
    public enum IPType
    {
        Unknown,
        Native,
        Toolkit
    }


    public class IntegrationPack
    {

        public IntegrationPack ()
        {
        }

        private string _name;
        private string _productID;
        private string _resourceFile;
        private Category[] _categories;
        private Configuration[] _configurations;
        private string[] _dependentFiles;

        public void AddCategory()
        {
        }

        public void AddActivity()
        {
        }

        public void AddConfiguration()
        {
        }

    }
}
