using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Runtime.Serialization;

public static class DirectoryInfoExtensions
{
    public static DateTime GetOldestDate(this DirectoryInfo directory)
    {
        DateTime oldestDate = DateTime.MaxValue;

        foreach (var file in directory.GetFiles())
        {
            if (file.LastWriteTime < oldestDate)
            {
                oldestDate = file.LastWriteTime;
            }
        }

        foreach (var subdirectory in directory.GetDirectories())
        {
            DateTime subdirectoryOldestDate = GetOldestDate(subdirectory);

            if (subdirectoryOldestDate < oldestDate)
            {
                oldestDate = subdirectoryOldestDate;
            }
        }

        return oldestDate;
    }
    public static string fileinfo(this FileSystemInfo info)
    {
        char readOnly = (info.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? 'r' : '-';
        char archive = (info.Attributes & FileAttributes.Archive) == FileAttributes.Archive ? 'a' : '-';
        char hidden = (info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? 'h' : '-';
        char system = (info.Attributes & FileAttributes.System) == FileAttributes.System ? 's' : '-';
        return new string(new[] { readOnly, archive, hidden, system });
    }
    public static string direinfo(this DirectoryInfo directory)
    {
        char readOnly = (directory.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? 'r' : '-';
        char archive = (directory.Attributes & FileAttributes.Archive) == FileAttributes.Archive ? 'a' : '-';
        char hidden = (directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? 'h' : '-';
        char system = (directory.Attributes & FileAttributes.System) == FileAttributes.System ? 's' : '-';
        return new string(new[] { readOnly, archive, hidden, system });
    }
    // w jaki sposob mam sposob sortowania w kolekcji



}


namespace ConsoleApp2
{
    [Serializable]
    class DictionaryComparator : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            if (a.Length > b.Length)
                return 1;
            if (a.Length < b.Length)
                return -1;
            return a.CompareTo(b);
        }
    }


    public static class Program
    {

        static void Main(string[] args)
        {

            string path = args[0];
            direloop(path, 0, "");
            Console.WriteLine();

            DirectoryInfo zad3a = new DirectoryInfo(path);
            Console.WriteLine("Najstarszy plik:" + zad3a.GetOldestDate());
            Console.WriteLine();
            CreateCollection(path);
            Console.WriteLine();

        }
        static void print(string path, int count)
        {
            count++;
            string tab = "";
            for (int i = 0; i < count; i++)
                tab += "\t";


            foreach (string file in Directory.GetFiles(path))
            {

                FileInfo zad3b = new FileInfo(file);

                string attributes = zad3b.fileinfo();
                string lastElement = Path.GetFileName(file);
                Console.WriteLine(tab + lastElement + " " + zad3b.Length + " "+ attributes);

            }
            string[] directoryies = Directory.GetDirectories(path);
            foreach (string dire in directoryies)
            {
                direloop(dire, count, tab);
            }

        }
        static void direloop(string dire, int count, string tab)
        {
            DirectoryInfo zad3b = new DirectoryInfo(dire);
            string attributes = zad3b.direinfo();
            int filesCount = (zad3b.GetDirectories().Length + zad3b.GetFiles().Length);
            string lastElement = Path.GetFileName(dire);
            Console.WriteLine(tab + lastElement + " (" + filesCount + ") " + attributes);
            print(dire, count);
        }

        static void Deserialize()
        {
            SortedDictionary<string, long> collection = null;
            FileStream fs = new FileStream("zapis.dat", FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                collection = (SortedDictionary<string, long>)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Fail Deserialize: " + e.Message);
                throw;
            }
            foreach (KeyValuePair<string, long> file in collection)
            {
                Console.WriteLine("{0} -> {1}", file.Key, file.Value);
            }
            fs.Close();

        }
        public static void CreateCollection(string path)
        {
            SortedDictionary<string, long> collection = new SortedDictionary<string, long>(new DictionaryComparator());
            foreach (string f in Directory.GetFiles(path))
            {
                FileInfo file = new FileInfo(f);
                collection.Add(Path.GetFileName(f), file.Length);
            }
            foreach (string c in Directory.GetDirectories(path))
            {
                DirectoryInfo directory = new DirectoryInfo(c);
                long size = directory.GetDirectories().Length + directory.GetFiles().Length;
                if (!collection.ContainsKey(c))
                {
                    collection.Add(Path.GetFileName(c), size);
                }

            }

            // SERIALIZATION
            FileStream filesave = new FileStream("zapis.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(filesave, collection);
            }
            catch(SerializationException exit)
            {
                Console.WriteLine("error" + exit.Message);
                throw;
            }
            filesave.Close();
            Deserialize();
        }
    }
}
