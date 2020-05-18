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
            // handle to mainWindow
            mW = parent;

            InitializeComponent();

            // set up dropdownmenu items for drives
            drives = DriveInfo.GetDrives();
            for (int i = 0; i < drives.Length; i++)
            {
                driveSelector.Items.Add(drives[i].Name);
            }
            driveSelector.SelectedIndex = 0;

            // initialize install folder text
            setInstallText(driveSelector.SelectedItem.ToString());
        }

        private void installDirTextBox_TextChanged(object sender,
            TextChangedEventArgs e)
        {
            // this will act as a common callback to update program status,
            // runs when install dir textbox text has changed
            mW.setInstallDir(installDirTextBox.Text);
            updateDirectories();
        }

        private void setInstallText(string txt)
        {
            installDirTextBox.Text = txt;
        }

        private void updateDirectories()
        {
            // empty filetree listbox
            fileTreeListBox.Items.Clear();

            // check that there is something reasonable written as the dir,
            // if not, make the listbox empty and return
            if (mW.getInstallDir().Length < 3)
            {
                return;
            }

            // get information+parentdir of the install directory
            DirectoryInfo dirinfo = new DirectoryInfo(mW.getInstallDir());
            DirectoryInfo installdirParent = dirinfo.Parent;

            string dirname;
            DirectoryInfo[] dirs;

            // if the directory doesnt exist, return and leave listbox
            // empty
            if (!dirinfo.Exists)
            {
                return;
            }

            // get directories and catch possible errors with
            // e.g. protected or unmounted directories
            try
            {
                dirs = dirinfo.GetDirectories();
            }
            catch
            {
                // just very basic error handling
                MessageBox.Show("Some error happened.");
                if (installdirParent != null)
                {
                    setInstallText(installdirParent.FullName);
                }
                return;
            }

            // if the directory does not have a parent
            // means its at root directory
            if (installdirParent == null)
            {
                // location is drive root
                for (int i = 0; i < dirs.Length; i++)
                {
                    // dont add ".."
                    dirname = dirs[i].Name;
                    fileTreeListBox.Items.Add(dirname);
                }
            }
            else
            {
                // add directory listbox items
                fileTreeListBox.Items.Add("..");
                for (int i = 0; i < dirs.Length; i++)
                {
                    dirname = System.IO.Path.GetFileName(dirs[i].Name);
                    fileTreeListBox.Items.Add(dirname);
                }
            }
        }

        private void fileTreeListBox_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(mW.getInstallDir());
            DirectoryInfo parentDir = dirinfo.Parent;

            // check if dirinfo has a parent or is it filetree root
            if (parentDir == null)
            {
                // we are at root, no .. present
                setInstallText(System.IO.Path.Combine(
                    drives[driveSelector.SelectedIndex].Name,
                    fileTreeListBox.SelectedItem.ToString()));
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
                    // combine the installdir and filetree item
                    // (we know its a valid directory bcause
                    // of error checks in updateDirectories()
                    setInstallText(System.IO.Path.Combine(mW.getInstallDir(),
                        fileTreeListBox.SelectedItem.ToString()));
                }
            }
        }

        private void driveSelector_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
            // when drive has been changed, reset to the root of
            // that drives filetree
        {
            setInstallText(driveSelector.SelectedItem.ToString());
        }
    }
}
