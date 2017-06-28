using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.PowerShell.Host.ISE;
using Microsoft.Windows.PowerShell.Gui.Internal;

namespace PoSH_ISE_SMA_Browser
{
    /// <summary>
    /// Interaction logic for SMABrowserPane.xaml
    /// </summary>
    public partial class SMABrowserPane : UserControl, IAddOnToolHostObject, INotifyPropertyChanged
    {
        private ObjectModelRoot _hostObject;
        private TreeViewItem _rootNode = new TreeViewItem() { Header = "Root" };
        public TreeViewItem RootNode
        {
            get { return _rootNode; }
            set { _rootNode = value; }
        }
        public SMABrowserPane()
        {
            InitializeComponent();
            
            List<string> tagList = new List<string>();
            tagList.Add("Testing");
            tagList.Add("Resting");
            tagList.Add("Track");
            tagList.Add("1");

            XmlDataProvider dp = (XmlDataProvider)this.FindResource("xmlDP");

        }

        public ObjectModelRoot HostObject
        {
            get { return _hostObject; }
            set
            {
                _hostObject = value;
                var handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs("HostObject"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
