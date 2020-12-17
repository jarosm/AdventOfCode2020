using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D17
{
    class Program
    {
        static int[,,,] space, newspace;

        static int CountSumForCube(int w, int k, int j, int i)
        {
            int sum = space[w, k - 1, j - 1, i - 1] + space[w, k - 1, j - 1, i] + space[w, k - 1, j - 1, i + 1]
                + space[w, k - 1, j, i - 1] + space[w, k - 1, j, i] + space[w, k - 1, j, i + 1]
                + space[w, k - 1, j + 1, i - 1] + space[w, k - 1, j + 1, i] + space[w, k - 1, j + 1, i + 1]

                + space[w, k, j - 1, i - 1] + space[w, k, j - 1, i] + space[w, k, j - 1, i + 1]
                + space[w, k, j, i - 1] + space[w, k, j, i] + space[w, k, j, i + 1]
                + space[w, k, j + 1, i - 1] + space[w, k, j + 1, i] + space[w, k, j + 1, i + 1]

                + space[w, k + 1, j - 1, i - 1] + space[w, k + 1, j - 1, i] + space[w, k + 1, j - 1, i + 1]
                + space[w, k + 1, j, i - 1] + space[w, k + 1, j, i] + space[w, k + 1, j, i + 1]
                + space[w, k + 1, j + 1, i - 1] + space[w, k + 1, j + 1, i] + space[w, k + 1, j + 1, i + 1];
            return sum;
        }


        static void ProcessCycle(int w1, int w2, int z1, int z2, int y1, int y2, int x1, int x2)
        {
            for (int m = w1; m < w2; m++)
            {
                for (int k = z1; k < z2; k++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        for (int i = x1; i < x2; i++)
                        {
                            int sum = 0;
                            for (int c = -1; c <= 1; c++)
                            {
                                sum += CountSumForCube(m + c, k, j, i);
                            }
                            sum -= space[m, k, j, i];                            

                            if (space[m, k, j, i] == 1)
                            {
                                if ((sum == 2) || (sum == 3))
                                    newspace[m, k, j, i] = 1;
                                else
                                    newspace[m, k, j, i] = 0;
                            }
                            else
                            {
                                if (sum == 3)
                                    newspace[m, k, j, i] = 1;
                                else
                                    newspace[m, k, j, i] = 0;
                            }
                        }
                    }
                }
            }
        }


        static private int D17(bool part2)
        {
            int cycles = 6;
            List<string> lines = new List<string>();

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D17\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            space = new int[2 * cycles + 1 + 2, 2 * cycles + 1 + 2, 2 * cycles + lines.Count + 2, 2 * cycles + lines[0].Length + 2];
            int w1 = cycles + 1, z1 = cycles + 1, y1 = cycles + 1, x1 = cycles + 1;
            int w2 = w1 + 1, z2 = z1 + 1, y2 = y1 + lines.Count, x2 = x1 + lines[0].Length;
            for (int j = 0; j < lines.Count; j++)
            {
                for (int i = 0; i < lines[j].Length; i++)
                {
                    space[w1, z1, y1 + j, x1 + i] = lines[j][i] == '.' ? 0 : 1;
                }
            }

            for (int c = cycles; c > 0; c--)
            {
                z1--; y1--; x1--;
                z2++; y2++; x2++;
                if (part2)
                {
                    w1--;
                    w2++;
                }

                newspace = new int[2 * cycles + 1 + 2, 2 * cycles + 1 + 2, 2 * cycles + lines.Count + 2, 2 * cycles + lines[0].Length + 2];
                ProcessCycle(w1, w2, z1, z2, y1, y2, x1, x2);
                space = newspace;
            }

            int sum = 0;
            for (int m = w1; m < w2; m++)
            {
                for (int k = z1; k < z2; k++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        for (int i = x1; i < x2; i++)
                        {
                            if (space[m, k, j, i] == 1)
                                sum++;
                        }
                    }
                }
            }
            return sum;
        }


        static void Main(string[] args)
        {
            int sum = D17(false);
            Console.WriteLine("Part 1: " + sum);

            sum = D17(true);
            Console.WriteLine("Part 2: " + sum);

            Console.WriteLine("end");
            Console.ReadLine();
        }
    }
}
