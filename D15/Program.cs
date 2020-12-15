using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D15
{
    class Program
    {
        static private int D15(int max)
        {
            int index = 0, lastnumber = 0, prevturn = -1;
            Dictionary<int, int> numbers = new Dictionary<int, int>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D15\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] t = line.Split(',');
                    for (int i = 0; i < t.Count(); i++)
                    {
                        lastnumber = Convert.ToInt32(t[i]);
                        numbers[lastnumber] = index;
                        index++;
                    }
                }
            }

            while (index < max)
            {
                if (prevturn == -1)
                    lastnumber = 0;
                else
                    lastnumber = index - 1 - prevturn;
                prevturn = numbers.ContainsKey(lastnumber) ? numbers[lastnumber] : -1;
                numbers[lastnumber] = index;

                index++;
            }

            return lastnumber;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Part 1: " + D15(2020));
            Console.WriteLine("Part 2: " + D15(30000000));
            Console.ReadLine();
        }
    }
}
