using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        string sourceDirectory;
        string workingDirectory;
        Page4 loaderStatusPage;

        // file transfer variables
        string[] files;

        public MainWindow()
        {
            // set private variable for page
            currentPage = 1;

            // resolve the program-files location
            workingDirectory = System.IO.Directory.GetCurrentDirectory();

            // resolve the path to source directory
            sourceDirectory = System.IO.Path.Combine(workingDirectory, "Files");

            // resolve all files that should be transfered
            files = System.IO.Directory.GetFiles(sourceDirectory, "*.*",
                SearchOption.AllDirectories);

            setInstallDir(workingDirectory);
            InitializeComponent();
            updateView();
        }


        public void moveFiles(Page4 loaderPage)
        {
            // loop through files
            for (int i = 0; i < files.Length; i++)
            {
                // define relative path of the file in source directory
                // (take away the beginning of path)
                string relativeFilePath = files[i].Substring(sourceDirectory.Length + 1);

                // create path to the destination file
                string destinationFile = System.IO.Path.Combine(installDirectory,
                    relativeFilePath);

                // create the directory/subdirectory, or if it exists, do nothing
                (new FileInfo(destinationFile)).Directory.Create();

                // copy one file
                System.IO.File.Copy(files[i], destinationFile, false);

                // update loaderPage status
                loaderPage.setStatus(i, files.Length);
                loaderPage.setCopiedFilename(destinationFile);
            }

            // move to next page
            currentPage += 1;
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
            currentPage -= 1;
            updateView();
        }

        private void Button_Forward_Click(object sender, RoutedEventArgs e)
        {
            currentPage += 1;
            updateView();
        }

        private void updateView()
        {
            // update navigation controls

            // backButton, dont display on some pages
            if (currentPage == 1 || currentPage > 3)
            {
                BackButton.Visibility = Visibility.Hidden;
            } else
            {
                BackButton.Visibility = Visibility.Visible;
            }

            // forwardButton, set to indicate quit on last page
            if (currentPage == 5)
            {
                ForwardButton.Content = "Quit";
            }

            // set page and perform action specific for that page
            if (currentPage == 1)
            {
                Main.Content = new Page1();
            } else if (currentPage == 2)
            {
                Main.Content = new Page2(this);
            } else if (currentPage == 3)
            {
                Main.Content = new Page3(this.getInstallDir(), files.Length);
            }
            else if (currentPage == 4)
            {
                loaderStatusPage = new Page4();
                Main.Content = loaderStatusPage;
            }
            else if (currentPage == 5)
            {
                Main.Content = new Page5();
            } 
            else if (currentPage == 6)
            {
                // when user presses the button on the last page, close application
                this.Close();
            }
        }

        private void Main_ContentRendered(object sender, EventArgs e)
        {
            //MessageBox.Show("ContentRendered");
            if (currentPage == 4)
            {
                moveFiles(loaderStatusPage);
            }
        }
    }
}
