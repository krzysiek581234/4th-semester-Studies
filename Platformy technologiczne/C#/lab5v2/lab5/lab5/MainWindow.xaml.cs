using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate int licznikdelegat(int n, int k);
        delegate int Mianownikdelegat(int k);
        zip gzip;
        public static int licznik(int n, int k)
        {
            int sum = 1;
            for (int i = n; i >= n - k + 1; i--)
            {
                sum = sum * i;
            }
            return sum;
        }
        public static int mianownik(int K)
        {
            int suma = 1;
            for (int i = 1; i <= K; i++)
            {
                suma = suma * i;
            }
            return suma;
        }
        static int zadanie_1a(int n, int k)
        {
            Task<int> tasklicznik = new Task<int>(() => licznik(n, k));
            Task<int> taskMianownik = new Task<int>(() => mianownik(k));
            tasklicznik.Start();
            taskMianownik.Start();
            Task.WhenAll(tasklicznik, taskMianownik);

            return tasklicznik.Result / taskMianownik.Result;
        }
        static void zadanie1_b(int n, int k)
        {
            licznikdelegat delegatetlicznik = licznik;
            Mianownikdelegat delegatetMianownik = mianownik;
            IAsyncResult d1 = delegatetlicznik.BeginInvoke(n, k, null, null);
            IAsyncResult d2 = delegatetMianownik.BeginInvoke(k, null, null);

            while (d1.IsCompleted == false || d2.IsCompleted == false) ;
            int ans = delegatetlicznik.EndInvoke(d1) / delegatetMianownik.EndInvoke(d2);
            Console.WriteLine(ans);
        }
        static async void zadanie_1c(int n, int k)
        {
            Task<int> taskMianownik = Task.Factory.StartNew<int>
               (
                    x =>
                    {
                        int K = (int)x;
                        return mianownik(K);
                    }, k //przyjemowane argumenty
               );
            Task<int> taskLicznik = Task.Factory.StartNew<int>
                (x =>
                {
                    Tuple<int, int> t = (Tuple<int, int>)x;
                    return licznik(t.Item1, t.Item2);
                },
                new Tuple<int, int>(n, k)

                );
            await Task.WhenAll(taskLicznik, taskMianownik);
            Console.WriteLine(taskLicznik.Result / taskMianownik.Result);
        }
        void GetFibClick(object senter, RoutedEventArgs f)
        {
            BackgroundWorker work = new BackgroundWorker();
            work.DoWork +=
                (
                    (senter, f) =>
                    {
                        int first = 1, secound = 1;
                        for (int i = 3; i <= (int)f.Argument; i++)
                        {
                            secound += first;
                            first = secound - first;
                            Thread.Sleep(20);
                            work.ReportProgress(100 * i / (int)f.Argument);
                            if (work.CancellationPending) return;
                        }
                        work.ReportProgress(100);
                        f.Result = secound;
                    }
                );
            work.WorkerSupportsCancellation = true;
            work.ProgressChanged +=
            (
                (object senter, ProgressChangedEventArgs args) =>
                {
                    this.ProgressBar.Value = args.ProgressPercentage;
                }
            );
            work.RunWorkerCompleted +=
            (
                (object senter, RunWorkerCompletedEventArgs f) =>
                {
                    this.FibWyniki.Content = f.Result;
                }
            );
            work.WorkerReportsProgress = true;
            int outx;
            if (Int32.TryParse(this.FibInput.Text, out outx))
                work.RunWorkerAsync(outx);
        }

        private void CompressClick(object senter, RoutedEventArgs rea)
        {
            this.gzip.doit(false);
        }

        private void DecompressClick(object senter, RoutedEventArgs rea)
        {
            gzip.doit(true);
        }
        public MainWindow()
        {
            this.gzip = new zip();
            
            InitializeComponent();
            int k = 5;
            int n = 15;

            Console.WriteLine(zadanie_1a(n, k));
            zadanie_1c(n, k);
            zadanie_1c(n, k);
        }
    }
}
