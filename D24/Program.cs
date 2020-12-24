using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D24
{
    class Program
    {
        private static int CountTiles(Dictionary<(int row, int col), bool> tiles, int row, int col)
        {
            int count = 0;

            count += tiles.ContainsKey((row + 1, col + 1)) ? 1 : 0; // ne
            count += tiles.ContainsKey((row, col + 1)) ? 1 : 0; // e
            count += tiles.ContainsKey((row - 1, col)) ? 1 : 0; // se
            count += tiles.ContainsKey((row - 1, col - 1)) ? 1 : 0; // sw
            count += tiles.ContainsKey((row, col - 1)) ? 1 : 0; // w
            count += tiles.ContainsKey((row + 1, col)) ? 1 : 0; // nw

            return count;
        }


        private static void D24()
        {
            var text = File.ReadAllText("d:\\programming\\Advent of Code\\data 2020\\D24\\input.txt");
            var instructions = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<(int row, int col), bool> blackTiles = new Dictionary<(int row, int col), bool>();
            int maxr = 0, maxc = 0, minr = 0, minc = 0;

            foreach (string line in instructions)
            {
                int r = 0, c = 0;
                string inst = line;
                while (inst.Length > 0)
                {
                    string direction;
                    if (inst.StartsWith("ne") || inst.StartsWith("se") || inst.StartsWith("nw") || inst.StartsWith("sw"))
                    {
                        direction = inst.Substring(0, 2);
                        inst = inst.Remove(0, 2);
                    }
                    else
                    {
                        direction = inst.Substring(0, 1);
                        inst = inst.Remove(0, 1);
                    }

                    switch (direction)
                    {
                        case "ne":
                            r += 1;
                            c += 1;
                            break;
                        case "e":
                            c += 1;
                            break;
                        case "se":
                            r -= 1;
                            break;
                        case "sw":
                            r -= 1;
                            c -= 1;
                            break;
                        case "w":
                            c -= 1;
                            break;
                        case "nw":
                            r += 1;
                            break;
                    }
                }

                if (blackTiles.ContainsKey((r, c)))
                    blackTiles.Remove((r, c));
                else
                    blackTiles.Add((r, c), true);

                if (r > 0 && r > maxr)
                    maxr = r;
                if (r < 0 && r < minr)
                    minr = r;
                if (c > 0 && c > maxc)
                    maxc = c;
                if (c < 0 && c < minc)
                    minc = c;
            }
            Console.WriteLine("Part 1: " + blackTiles.Count());


            for (int i = 1; i <= 100; i++)
            {
                maxr++; maxc++; minr--; minc--;

                Dictionary<(int row, int col), bool> newFloor = new Dictionary<(int row, int col), bool>();

                for (int r = minr; r <= maxr; r++)
                {
                    for (int c = minc; c <= maxc; c++)
                    {
                        int count = CountTiles(blackTiles, r, c);
                        if (blackTiles.ContainsKey((r, c)))
                        {
                            if (count == 1 || count == 2)
                                newFloor.Add((r, c), true);
                        }
                        else
                        {
                            if (count == 2)
                                newFloor.Add((r, c), true);
                        }
                    }
                }

                blackTiles = newFloor;
            }
            Console.WriteLine("Part 2: " + blackTiles.Count());


            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D24();
        }
    }
}
