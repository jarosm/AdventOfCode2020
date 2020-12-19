using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D20
{
    class Program
    {
        static private void D20a()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D20\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    if (line.Length > 0)
                        lines.Add(line);
                }
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D20a();
        }
    }
}
