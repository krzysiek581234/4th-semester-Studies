using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class zip
    {
        public zip() { }

        public int CompressFile(string filePath)
        {
            var fileBytes = File.ReadAllBytes(filePath);
            var compressedFilePath = filePath + ".gz";
            using (var compressedFileStream = File.Create(compressedFilePath))
            {
                using (var gzipStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    gzipStream.Write(fileBytes, 0, fileBytes.Length);
                }
            }

            return 0;
        }

        public int unzipfile(string plik, string path)
        {
            var bytes = File.ReadAllBytes(plik);
            FileInfo file = new FileInfo(path);
            string newName = System.IO.Path.GetFileNameWithoutExtension(plik);
            using (FileStream fileStream = new FileStream(path + "\\" + newName, FileMode.Create))
            using (FileStream fileSream2 = new FileStream(plik, FileMode.Open))
            using (GZipStream fileStream3 = new GZipStream(fileSream2, CompressionMode.Decompress, false))
            {
                //zipStream.Write(bytes, 0, bytes.Length);
                byte[] buf = new byte[1024];
                int nRead;
                while ((nRead = fileStream3.Read(buf, 0, buf.Length)) > 0)
                {
                    fileStream.Write(buf, 0, nRead);
                }
            }
            return 0;
        }
        public void doit(bool uncompress)
        {
            //var fileDialog = new Microsoft.Win32.OpenFileDialog();
            //fileDialog.Multiselect = true;

            if(uncompress)
            {
                string path;
                var fileDialog = new Microsoft.Win32.OpenFileDialog();
                fileDialog.Multiselect = true;
                fileDialog.Filter = "gzip files (*.gz)|*.gz";
                fileDialog.Title = "Choose multiple files";
                if (!(bool)fileDialog.ShowDialog())
                {
                    return;
                }
                path = Path.GetDirectoryName(fileDialog.FileName);
                string[] selectedFilePaths = fileDialog.FileNames;
                foreach (String x in selectedFilePaths)
                {
                    Task<int> task123 = new Task<int>(() => unzipfile(x, path));
                    task123.Start();
                }

            }
            else
            {
                string path;
                using (var folderBrose = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult wyn = folderBrose.ShowDialog();
                    if (wyn == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrose.SelectedPath))
                    {
                        path = folderBrose.SelectedPath;
                    }
                    else
                    {
                        return;
                    }
                }
                String[] tablist = Directory.GetFiles(path);
                foreach (String x in tablist)
                {
                    Task<int> task123 = new Task<int>(() => CompressFile(x));
                    task123.Start();
                }
            }
            


            //Task<int> tasklicznik = new Task<>(() => zipfile(fileDialog, k));
            //Task<int> taskMianownik = new Task<int>(() => mianownik(k));
            //tasklicznik.Start();
            //taskMianownik.Start();
            //Task.WhenAll(tasklicznik, taskMianownik);
        }
    }
}
