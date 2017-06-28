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
    public class Category
    {
        public Category()
        {
        }


        private string _name;
        private string _id;
        private string _description;
        private string _bitmap16;
        private string _bitmap32;
        private string _bitmapMask;
        private Activity[] _activities;

    }



}
