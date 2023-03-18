using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Forms;
using System.IO;
using System.Windows.Ink;


namespace lab2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open(object senter, RoutedEventArgs rea)
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //sukces
                treeView.Items.Clear();
                TreeViewItem root = AddElement(dlg.SelectedPath);
                treeView.Items.Add(root);
                Console.WriteLine("clik");
            }
        }
        private void Exit(object senter, RoutedEventArgs rea)
        {
            Close();
        }
        private TreeViewItem AddElement(string path)
        {
            if (Directory.Exists(path))
            {
                //self:
                DirectoryInfo dir = new DirectoryInfo(path);
                TreeViewItem element = new TreeViewItem();
                element.Header = dir.Name;
                element.Tag = dir.FullName;
                element.ContextMenu = new System.Windows.Controls.ContextMenu();
                System.Windows.Controls.MenuItem create = new System.Windows.Controls.MenuItem();
                System.Windows.Controls.MenuItem remove = new System.Windows.Controls.MenuItem();
                create.Header = "Create";
                remove.Header = "Remove";
                create.Click += new RoutedEventHandler(MenuCreate);
                remove.Click += new RoutedEventHandler(MenuRemove);
                element.ContextMenu.Items.Add(create);
                element.ContextMenu.Items.Add(remove);
                element.Selected += new RoutedEventHandler(UpdateBar);
                string[] files = Directory.GetFiles(path);
                foreach (string f in files)
                {
                    element.Items.Add(AddElement(f));
                }

                string[] directoryies = Directory.GetDirectories(path);
                foreach (string dire in directoryies)
                {
                    element.Items.Add( AddElement(dire));
                }



                return element;
            }
            else if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                TreeViewItem felement = new TreeViewItem();
                felement.Header = file.Name;
                felement.Tag = file.FullName;
                felement.ContextMenu = new System.Windows.Controls.ContextMenu();
                System.Windows.Controls.MenuItem open = new System.Windows.Controls.MenuItem();
                System.Windows.Controls.MenuItem fremove = new System.Windows.Controls.MenuItem();
                open.Header = "Open";
                fremove.Header = "Remove";
                open.Click += new RoutedEventHandler(MenuOpen);
                fremove.Click += new RoutedEventHandler(MenuRemove);
                felement.ContextMenu.Items.Add(open);
                felement.ContextMenu.Items.Add(fremove);
                felement.Selected += new RoutedEventHandler(UpdateBar);

                return felement;
            }
            else
            {
                return null;
            }

        }
        public void UpdateBar(object senter, RoutedEventArgs rea)
        {
            TreeViewItem selected = (TreeViewItem)treeView.SelectedItem;
            if (selected != null)
            {
                string parametry = "DOS:  ";
                string path = (string)selected.Tag;
                FileAttributes atrybuty = File.GetAttributes(path);
                if ((atrybuty & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    parametry += "r";
                else
                    parametry += "-";
                if ((atrybuty & FileAttributes.Archive) == FileAttributes.Archive)
                    parametry += "a";
                else
                    parametry += "-";
                if ((atrybuty & FileAttributes.System) == FileAttributes.System)
                    parametry += "s";
                else
                    parametry += "-";
                if ((atrybuty & FileAttributes.Hidden) == FileAttributes.Hidden)
                    parametry += "h";
                else
                    parametry += "-";

                paremetrybar.Text = parametry;
            }


        }
        private void MenuCreate(object senter, RoutedEventArgs rea)
        {
            //tworzy się nowy element -> nowe okno
            TreeViewItem selected = (TreeViewItem)treeView.SelectedItem;
            if (selected != null)
            {
                string path = (string)selected.Tag;

                tworzenieform window = new tworzenieform(path);
                window.ShowDialog();
                if (window.czyTak())
                {
                    string newPath = window.Getpath();
                    selected.Items.Add(AddElement(newPath));
                }
                
            }
        }
        private void MenuRemove(object senter, RoutedEventArgs rea)
        {
            TreeViewItem selected = (TreeViewItem)treeView.SelectedItem;
            if (selected != null)
            {
                string path = (string)selected.Tag;
                MenuRemoveFunction(path);

                //usunięcie z drzewa:
                if ((TreeViewItem)treeView.Items[0] != selected)
                {
                    TreeViewItem parent = (TreeViewItem)selected.Parent;
                    parent.Items.Remove(selected);
                }
                else // usuwanie roota
                {
                    treeView.Items.Clear();
                }
            }
        }
        private void MenuRemoveFunction(string path)
        {
            if (Directory.Exists(path))
            {

                FileAttributes atrybuty = File.GetAttributes(path);
                File.SetAttributes(path, atrybuty & ~FileAttributes.ReadOnly);

                foreach (string cpath in Directory.GetDirectories(path))
                {
                    MenuRemoveFunction(cpath);
                }
                foreach (string f in Directory.GetFiles(path))
                {
                    MenuRemoveFunction(f);
                }

                Directory.Delete(path);

            }
            else if (File.Exists(path))
            {
                FileAttributes atrybuty = File.GetAttributes(path);
                File.SetAttributes(path, atrybuty & ~FileAttributes.ReadOnly);
                File.Delete(path);
            }
        }
        private void MenuOpen(object senter, RoutedEventArgs rea)
        {
            TreeViewItem selected = (TreeViewItem)treeView.SelectedItem;
            if (selected != null)
            {
                String tx = File.ReadAllText((string)selected.Tag);
                StringBuilder sb = new StringBuilder();
                for (int i =0; i<tx.Length; i++)
                {
                    sb.Append(tx[i]);
                    if((i+1)%50 == 0)
                    {
                        sb.Append("\n");
                    }
                }
                Viewer.Content = new TextBlock()
                {
                    Text = sb.ToString()
                };
            }
        }
    }
}
