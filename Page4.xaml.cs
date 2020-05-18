using System.Windows.Controls;

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

            // add text to indicate current file in transfer
            statusText.Text = "Copying " + fname;

            // add loader bar and "ghost" loader bar behind it
            loadBar.Width = (float)fileind / (float)filecount * lbMax;
            loadBar2.Width = lbMax;
        }
    }
}
