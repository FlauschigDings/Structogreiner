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
            switch (greiner.Length)
            {
                case 0:
                    Console.WriteLine("0");
                    break;
                case 1:
                case 2:
                    Console.WriteLine("1 oder 2");
                    return;
                case 3 or 4:
                    Console.WriteLine("3 oder 4");
                    break;
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
