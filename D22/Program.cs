using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D22
{
    class Program
    {
        private static void D22a()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D22\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D22a();
        }
    }
}
