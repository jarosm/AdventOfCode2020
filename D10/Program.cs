using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D10
{
    class Program
    {
        static private void D10()
        {
            List<int> adapters = new List<int>();
            adapters.Add(0);
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D10\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    adapters.Add(Convert.ToInt32(line));
                }
            }

            List<int> sorted = adapters.OrderBy(a => a).ToList();
            List<int> diff = new List<int>();
            for (int i = 1; i < sorted.Count(); i++)
                diff.Add(sorted[i] - sorted[i - 1]);
            int one = diff.Count(a => a == 1);
            int three = diff.Count(a => a == 3) + 1;

            Console.WriteLine("Part 1: " + one * three);


            int device = sorted.Max() + 3;
            Dictionary<int, long> routes = new Dictionary<int, long> { { device, 1 } };
            foreach (int j in sorted.ToArray().Reverse())
            {
                routes.TryGetValue(j + 1, out long c1);
                routes.TryGetValue(j + 2, out long c2);
                routes.TryGetValue(j + 3, out long c3);
                routes[j] = c1 + c2 + c3;
            }

            Console.WriteLine("Part 2: " + routes[0]);


            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D10();
        }
    }
}
