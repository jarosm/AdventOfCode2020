using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1
{
    class Program
    {
        static private void D1a()
        {
            List<int> report = new List<int>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D1\\input.txt"))
            {
                string number = "";
                while ((number = input.ReadLine()) != null)
                {
                    try
                    {
                        report.Add(Convert.ToInt32(number));
                    }
                    catch { }
                }
            }

            bool found = false;
            for (int i = 0; i < report.Count - 1; i++)
            {
                for (int j = i + 1; j < report.Count; j++)
                {
                    if (report[i] + report[j] == 2020)
                    {
                        found = true;
                        Console.WriteLine(report[i] * report[j]);
                        break;
                    }
                }
                if (found)
                    break;
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }

        static private void D1b()
        {
            List<int> report = new List<int>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D1\\input.txt"))
            {
                string number = "";
                while ((number = input.ReadLine()) != null)
                {
                    try
                    {
                        report.Add(Convert.ToInt32(number));
                    }
                    catch { }
                }
            }

            bool found = false;
            for (int i = 0; i < report.Count - 2; i++)
            {
                for (int j = i + 1; j < report.Count - 1; j++)
                {
                    for (int k = j + 1; k < report.Count(); k++)
                    {
                        if (report[i] + report[j] + report[k] == 2020)
                        {
                            found = true;
                            Console.WriteLine(report[i] * report[j] * report[k]);
                            break;
                        }
                    }
                    if (found)
                        break;
                }
                if (found)
                    break;
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D1a();

            D1b();
        }

    }
}
