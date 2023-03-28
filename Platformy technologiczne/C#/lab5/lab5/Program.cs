using System;
using System.Threading.Tasks;

namespace lab5
{
    class Program
    {
        public static int zad1a(int a, int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += a;
                a = a * 2;
            }

            return sum;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            Task<int> taskD = new Task<int>(() => zad1a(2, 10));
            taskD.Start();
            taskD.Wait();
            Console.WriteLine(taskD.Result);
            Console.WriteLine("aa");
        }


    }
}
