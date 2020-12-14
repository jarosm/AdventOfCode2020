using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D14
{
    class Program
    {
        static private string ApplyMask(string val, string mask)
        {
            string result = "";
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                    result += val[i];
                else
                    result += mask[i];
            }
            return result;
        }


        static private IEnumerable<string> ApplyMaskToAddress(string address, string mask)
        {
            string result = "";

            int combinations = (int)Math.Pow(2, mask.Count(c => c == 'X'));

            for (int i = 0; i < combinations; i++)
            {
                result = "";
                string ibin = Convert.ToString(i, 2);
                int pos = ibin.Length - 1;

                for (int m = mask.Length - 1; m >= 0; m--)
                {
                    if (mask[m] == '0')
                        result = address[m] + result;
                    else
                    {
                        if (mask[m] == '1')
                            result = '1' + result;
                        else // X
                        {
                            if (pos >= 0)
                            {
                                result = ibin[pos] + result;
                                pos--;
                            }
                            else
                                result = '0' + result;
                        }
                    }
                }

                yield return result;
            }
        }


        static private void D14a()
        {
            Dictionary<ulong, ulong> mem = new Dictionary<ulong, ulong>();
            string mask = "";
            int maskLength = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D14\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    if (line.StartsWith("mask"))
                    {
                        mask = line.Replace("mask = ", "");
                        maskLength = mask.Length;
                    }
                    else
                    {
                        string[] t = line.Replace("mem[", "").Replace("] = ", ";").Split(';');
                        string binString = Convert.ToString(Convert.ToInt64(t[1]), 2);
                        while (binString.Length < maskLength)
                            binString = "0" + binString;
                        binString = ApplyMask(binString, mask);
                        mem[Convert.ToUInt64(t[0])] = Convert.ToUInt64(binString, 2);
                    }
                }
            }

            Console.WriteLine("Part 1: " + mem.Aggregate(0UL, (a, c) => a + c.Value));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D14b()
        {
            Dictionary<ulong, ulong> mem = new Dictionary<ulong, ulong>();
            string mask = "";
            int maskLength = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D14\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    if (line.StartsWith("mask"))
                    {
                        mask = line.Replace("mask = ", "");
                        maskLength = mask.Length;
                    }
                    else
                    {
                        string[] t = line.Replace("mem[", "").Replace("] = ", ";").Split(';');

                        string binString = Convert.ToString(Convert.ToInt64(t[0]), 2);
                        while (binString.Length < maskLength)
                            binString = "0" + binString;

                        foreach (string s in ApplyMaskToAddress(binString, mask))
                            mem[Convert.ToUInt64(s, 2)] = Convert.ToUInt64(t[1]);
                    }
                }
            }

            Console.WriteLine("Part 2: " + mem.Aggregate(0UL, (a, c) => a + c.Value));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D14a();

            D14b();
        }
    }
}
