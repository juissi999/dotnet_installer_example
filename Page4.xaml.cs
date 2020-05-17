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
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4(double lbMax, string fname, int fileind, int filecount)
        {
            InitializeComponent();
            statusText.Text = "Copying " + fname;
            loadBar.Width = (float)fileind / (float)filecount * lbMax;
            loadBar2.Width = lbMax;
        }
    }
}
