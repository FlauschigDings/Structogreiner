using System;

namespace ParseTest
{
    static class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 2, 5, -1, 9, 4, 6, 123, 100 };
            Sort(arr);
            Console.WriteLine(string.Join(", ", arr));

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
            while (1 < 10)
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
            while (true)
            {
                a = "fsadjiosdaojpdsa";
            }
            return a;
        }

        static string Switch()
        {
            var a = "dsasad";
            Console.WriteLine(a);
            switch (a)
            {
                case "dsasdasdasa":
                    a = "hallo";
                    a = "ssss";
                    Test5();
                    break;
                case "asdsaddsadssda":
                    Test100();
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

        static void Sort(int[] numbers)
        {
            for (var i = 1; i < numbers.Length; i++)
            {
                while (numbers[i - 1] > numbers[i])
                {
                    var temp = numbers[i - 1];
                    numbers[i - 1] = numbers[i];
                    numbers[i] = temp;
                    if (i > 1) i --;
                }
            }
        }
    }
}
