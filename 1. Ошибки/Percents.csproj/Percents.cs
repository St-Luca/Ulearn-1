using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            double output = Calculate(input);

            Console.WriteLine(output);
            Console.ReadLine();
        }

        public static double Calculate(string input)
        {
            double sum = double.Parse(input.Split(' ')[0]);
            double percent = double.Parse(input.Split(' ')[1]) / 100;
            int monthes = int.Parse(input.Split(' ')[2]);

            for (int i = 1; i <= monthes; i++)
            {
                sum += sum * percent / 12;
            }
            return sum;
        }
    }
}
