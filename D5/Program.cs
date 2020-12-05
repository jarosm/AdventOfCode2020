using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D5
{
    class Program
    {
        static private void D5a()
        {
            int maxId = 0;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D5\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    int id = Convert.ToUInt16(line.Substring(0, 7).Replace('F', '0').Replace('B', '1'), 2) * 8 + Convert.ToUInt16(line.Substring(7, 3).Replace('L', '0').Replace('R', '1'), 2);

                    if (id > maxId)
                        maxId = id;
                }
            }

            Console.WriteLine(maxId);

            Console.WriteLine("end");
            Console.ReadLine();
        }

        static private void D5b()
        {
            bool[] seats = new bool[1024];

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D5\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    int id = Convert.ToUInt16(line.Substring(0, 7).Replace('F', '0').Replace('B', '1'), 2) * 8 + Convert.ToUInt16(line.Substring(7, 3).Replace('L', '0').Replace('R', '1'), 2);

                    seats[id] = true;
                }
            }

            for (int i = 0; i < 1024; i++)
            {
                if (!seats[i])
                    Console.Write(i + " ; ");
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D5a();

            D5b();
        }
    }
}
