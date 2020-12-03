using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    class Program
    {
        static List<string> lines = new List<string>();

        static private int CountTrees(int slopeColCount, int slopeRowCount)
        {
            int count = 0, row = 0, col = 0, rowmax = lines.Count() - 1, colmax = lines[0].Length;

            while (row < rowmax)
            {
                col += slopeColCount;
                if (col >= colmax)
                    col -= colmax;
                row += slopeRowCount;
                if (lines[row][col] == '#')
                    count++;
            }

            return count;
        }


        static private void D3a()
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D3\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                    lines.Add(line);
            }

            int count = 0;

            count = CountTrees(3, 1);

            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D3b()
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D3\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                    lines.Add(line);
            }

            Int64 count = 0;

            count = CountTrees(1, 1);
            count *= CountTrees(3, 1);
            count *= CountTrees(5, 1);
            count *= CountTrees(7, 1);
            count *= CountTrees(1, 2);

            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D3a();

            D3b();
        }
    }
}
