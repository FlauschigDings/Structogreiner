using System;

namespace ParseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var greiner = "Greiner";
            var greinerGedreht = "";

            for (int i = greiner.Length - 1; i >= 0; i--)
            {
                greinerGedreht += greiner[i];
            }
        }

        static string Test()
        {
            var a = "dsasad";
            Console.WriteLine(a);
            return a;
        }
    }
}
