using System;

namespace ParseTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var greiner = "Greiner";
            var greinerGedreht = "";
            While();

            for (int i = greiner.Length - 1; i >= 0; i--)
            {
                greinerGedreht += greiner[i];
            }
        }

        static string While()
        {
            var a = "dsasad";
            while(1 < 10)
            {
                a = "xxx";
            }
            return a;
        }

        static string DoWhile()
        {
            var a = "dsasad";
            do
            {
                a = "sfdajiosadplkdsalkdsalködsaklödsa";
            } while (1 < 10);
            return a;
        }

        static string WhileTrue()
        {
            var a = "dsasad";
            while(true)
            {
                a = "fsadjiosdaojpdsa";
            }
            return a;
        }

        static string Switch()
        {
            var a = "dsasad";
            Console.WriteLine(a);
            switch(a) {
                case "dsasdasdasa":
                    a = "hallo";
                    break;
                case "asdsaddsadssda":
                    break;
                default:
                    break;
            }
            return a;
        }

        static string Test5()
        {
            var a = "dsasad";
            Console.WriteLine(a);
            return a;
        }

        static string Test100()
        {
            var a = "dsasad";
            Console.WriteLine(a);
            return a;
        }
        static string greindern()
        {
            var a = "dsasad";
            Console.WriteLine(a);
            return a;
        }
    }
}
