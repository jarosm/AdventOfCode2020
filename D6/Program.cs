using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D6
{
    class Program
    {
        static private void D6a()
        {
            int count = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D6\\input.txt"))
            {
                string line = "";
                int[] answers = new int[26];
                while ((line = input.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        count += answers.Count(a => a == 1);
                        Array.Clear(answers, 0, 26);
                    }
                    else
                    {
                        for (int i = 0; i < line.Length; i++)
                            answers[(int)line[i] - 97] = 1;
                    }
                }
                count += answers.Count(a => a == 1);
            }
            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }

        static private void D6b()
        {
            int count = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D6\\input.txt"))
            {
                string line = "";
                int[] answers = new int[26];
                int peopleingroup = 0;
                while ((line = input.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        count += answers.Count(a => a == peopleingroup);
                        Array.Clear(answers, 0, 26);
                        peopleingroup = 0;
                    }
                    else
                    {
                        peopleingroup++;
                        for (int i = 0; i < line.Length; i++)
                            answers[(int)line[i] - 97] += 1;
                    }
                }
                count += answers.Count(a => a == peopleingroup);
            }
            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D6a();

            D6b();
        }
    }
}
