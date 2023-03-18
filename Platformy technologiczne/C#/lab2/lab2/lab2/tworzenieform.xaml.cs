using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Logika interakcji dla klasy tworzenieform.xaml
    /// </summary>
    public partial class tworzenieform : Window
    {
        private String path;
        private bool tak;
        public tworzenieform(String path)
        {
            InitializeComponent();
            this.path = path;
            tak = false;
        }
        public bool czyTak()
        {
            return tak;
        }
        public String Getpath()
        {
            return path;
        }
        public void okclick(object senter, RoutedEventArgs rea)
        {
            this.tak = true;
            bool folder = false;
            string nazwa = NewName.Text;
            this.path = this.path + "\\" + nazwa;
            if ((bool)Filee.IsChecked)
            {
                if(Regex.IsMatch(NewName.Text, "^[a-zA-Z0-9_~-]{1,8}\\.(txt|php|html)$"))
                {
                    folder = true;
                    File.Create(this.path);
                }
                else
                {
                    System.Windows.MessageBox.Show("Wrong name!", "Why Though?", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    return;
                }
            }
            else if ((bool)Directoryy.IsChecked)
            {
                folder = false;
                Directory.CreateDirectory(path);
            }
            else
            {
                //abort
                System.Windows.MessageBox.Show("Choose whether you want dir or file first!", "Why?", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }
            // ATRYBUTY
            FileAttributes atrybuty = (FileAttributes)128;
            //ReadOnly	1	
            //Hidden	2	
            //System	4
            //Archive	32
            //Normal	128

            if ((bool)cReadOnly.IsChecked)
            {
                atrybuty |= FileAttributes.ReadOnly;
            }
            else
            {
                atrybuty &= ~(FileAttributes.ReadOnly);
            }

            if ((bool)cHidden.IsChecked)
            {
                atrybuty |= FileAttributes.Hidden;
            }
            else
            {
                atrybuty &= ~(FileAttributes.Hidden);
            }

            if ((bool)cSystem.IsChecked)
            {
                atrybuty |= FileAttributes.System;
            }
            else
            {
                atrybuty &= ~(FileAttributes.System);
            }

            if ((bool)cArchive.IsChecked)
            {
                atrybuty |= FileAttributes.Archive;
            }
            else
            {
                atrybuty &= ~(FileAttributes.Archive);
            }
            File.SetAttributes(path, atrybuty);
            tak = true;
            Close();

        }
        public void cancel(object senter, RoutedEventArgs rea)
        {
            this.tak = false;
            Close();
        }

    }
}
