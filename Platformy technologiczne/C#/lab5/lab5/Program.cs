using System;
using System.Threading.Tasks;

namespace lab5
{
    class Program
    {
        delegate int licznikdelegat(int n, int k);
        delegate int Mianownikdelegat(int k);
        public static int licznik(int n, int k)
        {
            int sum = 1;
            for(int i=n; i >= n-k+1;i--)
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
        static void zadanie1_b(int n,int k)
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
                new Tuple<int,int>(n,k)
                
                );
            await Task.WhenAll(taskLicznik, taskMianownik);
            Console.WriteLine(taskLicznik.Result / taskMianownik.Result);
        }
        static void Main(string[] args)
        {
            int k = 5;
            int n = 15;

            Console.WriteLine(zadanie_1a(n,k));
            zadanie_1c(n,k);
            zadanie_1c(n, k);
        }


    }
}
