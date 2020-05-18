using System.Windows.Controls;

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
