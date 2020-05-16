using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentPage;
        string installDirectory;

        public MainWindow()
        {
            // set private variable for page
            currentPage = 1;
            setInstallDir(System.IO.Directory.GetCurrentDirectory());
            InitializeComponent();
            updateView();
        }

        public void setInstallDir(string newDir)
        {
            installDirectory = newDir;
        }

        public string getInstallDir()
        {
            return installDirectory;
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = null;
            currentPage -= 1;
            updateView();
        }
        private void Button_Forward_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = null;
            currentPage += 1;
            updateView();
        }

        private void updateView()
        {
            // update navigation controls
            if (currentPage == 1)
            {
                BackButton.Visibility = Visibility.Hidden;
            } else
            {
                BackButton.Visibility = Visibility.Visible;
            }
                // set page
            if (currentPage == 1)
            {
                Main.Content = new Page1();
            } else if (currentPage == 2)
            {
                Main.Content = new Page2(this);
            } else if (currentPage == 3)
            {
                Main.Content = new Page3(this.getInstallDir());
            }
        }

    }
}
