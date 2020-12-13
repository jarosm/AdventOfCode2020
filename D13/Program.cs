using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D13
{
    class Program
    {
        static private void D13a()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D13\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            int timestamp = Convert.ToInt32(lines[0]);
            lines[1] = lines[1].Replace("x,", "");
            foreach (string bus in lines[1].Split(','))
            {
                int busNumber = Convert.ToInt32(bus);
                int minutesToDepart = busNumber - (timestamp % busNumber);
                Console.WriteLine("Bus number: {0} ; Minutes to depart: {1} ; Result: {2}", busNumber, minutesToDepart, busNumber * minutesToDepart);
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D13b()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D13\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            string[] t1 = lines[1].Split(',');
            List<(long modulo, long remainder)> primes = new List<(long modulo, long remainder)>();
            for (int i = 0; i < t1.Length; i++)
            {
                if (t1[i] == "x")
                    continue;
                primes.Add((Convert.ToInt32(t1[i]), i > 0 ? (Convert.ToInt32(t1[i]) > i ? Convert.ToInt32(t1[i]) - i : Convert.ToInt32(t1[i]) - i % Convert.ToInt32(t1[i])) : i));
            }            

            long a = primes[0].remainder;
            long n = primes[0].modulo;
            for (int i = 1; i < primes.Count(); i++)
            {
                long x = FindX(a, n, primes[i].modulo, primes[i].remainder);
                Console.WriteLine("Part 2 - sub goal: " + x);
                a = x;
                n = n * primes[i].modulo;
            }
            Console.WriteLine("Part 2: " + a);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private long FindX(long a, long n, long modulo, long remainder)
        {
            long x = a;
            while (x % modulo != remainder)
                x += n;
            return x;
        }


        static void Main(string[] args)
        {
            //D13a();

            D13b();
        }
    }
}
