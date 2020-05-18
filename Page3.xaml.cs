using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotnet_installer_example
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3(string installDir, int fileCount)
        {
            InitializeComponent();

            // set query text with install directory and filecount
            installQueryTxt.Text = "Install " + fileCount.ToString() +
                " files to\n" + installDir + "?" +
                "\n\nClick Forward to start install.";
        }
    }
}
