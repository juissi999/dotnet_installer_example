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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        MainWindow mW;
        public Page2(MainWindow parent)
        {
            mW = parent;
            InitializeComponent();
            installDirTextBox.Text = mW.getInstallDir();
        }

        private void installDirTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mW.setInstallDir(installDirTextBox.Text);
        }
    }
}
