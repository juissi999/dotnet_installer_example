using Microsoft.Win32;
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
using System.IO;

namespace dotnet_installer_example
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        MainWindow mW;
        DriveInfo[] drives;
        public Page2(MainWindow parent)
        {
            mW = parent;
            InitializeComponent();
            //setInstallText(mW.getInstallDir());

            // set up dropdownmenu for drives
            drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                driveSelector.Items.Add(drives[i].Name);
            }
            driveSelector.SelectedIndex = 0;
            setInstallText(driveSelector.SelectedItem.ToString());
        }

        private void installDirTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mW.setInstallDir(installDirTextBox.Text);
            updateDirectories();
        }

        private void setInstallText(string txt)
        {
            installDirTextBox.Text = txt;
        }


        private void updateDirectories()
        {
            fileTreeListBox.Items.Clear();
            // check that there is something reasonable written as the dir
            if (mW.getInstallDir().Length < 3)
            {
                return;
            }

            DirectoryInfo dirinfo = new DirectoryInfo(mW.getInstallDir());
            DirectoryInfo installdirParent = dirinfo.Parent;

            string dirname;
            DirectoryInfo[] dirs;

            // string[] files = System.IO.Directory.GetDirectories(mW.getInstallDir());
            if (!dirinfo.Exists)
            {
                return;
            }

            try
            {
                dirs = dirinfo.GetDirectories();
            }
            catch
            {
                MessageBox.Show("Some error happened.");
                if (installdirParent != null)
                {
                    setInstallText(installdirParent.FullName);
                }
                return;
            }


            if (installdirParent == null)
            {
                // location is drive root
                for (int i = 0; i < dirs.Length; i++)
                {
                    dirname = dirs[i].Name;
                    fileTreeListBox.Items.Add(dirname);
                }
            }
            else
            {
                fileTreeListBox.Items.Add("..");
                for (int i = 0; i < dirs.Length; i++)
                {
                    dirname = System.IO.Path.GetFileName(dirs[i].Name);
                    fileTreeListBox.Items.Add(dirname);
                }
            }
        }

        private void fileTreeListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(mW.getInstallDir());

            DirectoryInfo parentDir = dirinfo.Parent;
            if (parentDir == null)
            {
                // we are at root, no .. present
                setInstallText(System.IO.Path.Combine(drives[driveSelector.SelectedIndex].Name, fileTreeListBox.SelectedItem.ToString()));
            }
            else
            {
                if (fileTreeListBox.SelectedIndex == 0)
                {
                    // up a dir
                    setInstallText(parentDir.FullName);
                }
                else
                {
                    setInstallText(System.IO.Path.Combine(parentDir.FullName, fileTreeListBox.SelectedItem.ToString()));
                }
            }
        }

        private void driveSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setInstallText(driveSelector.SelectedItem.ToString());
        }
    }
}
