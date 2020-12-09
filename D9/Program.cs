using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D9
{
    class Program
    {
        static int preambleLenght = 25;

        static private void D9()
        {
            List<(long value, long[] sums)> data = new List<(long value, long[] sums)>();
            int index = 0;
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D9\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    data.Add((Convert.ToInt64(line), new long[preambleLenght - 1]));

                    if (index >= preambleLenght)
                    {
                        bool found = false;
                        int t = preambleLenght - 2;
                        for (int i = Math.Max(1, index - preambleLenght + 1); i < index; i++)
                        {
                            for (int j = t; j < preambleLenght - 1; j++)
                            {
                                if (data[index].value == data[i].sums[j])
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                                break;
                            t--;
                        }
                        if (!found)
                            break;
                    }

                    int s = index >= preambleLenght ? 0 : preambleLenght - index - 1;
                    for (int i = Math.Max(0, index - preambleLenght + 1); i < index; i++)
                    {
                        data[index].sums[s] = data[index].value + data[i].value;
                        s++;
                    }

                    index++;
                }
            }

            long weakness = data[index].value;
            long encweakness = 0, temp = 0, min = long.MaxValue, max = long.MinValue;

            for (int i = 0; i < index; i++)
            {
                int j = i;
                while (j < index)
                {
                    temp += data[j].value;
                    if (data[j].value < min)
                        min = data[j].value;
                    if (data[j].value > max)
                        max = data[j].value;
                    if (temp >= weakness)
                        break;
                    j++;
                }
                if (temp == weakness)
                {
                    encweakness = min + max;
                    break;
                }
                else
                {
                    temp = 0;
                    min = long.MaxValue;
                    max = long.MinValue;
                }
            }

            Console.WriteLine("Weakness number: " + weakness);
            Console.WriteLine("Encryption weakness: " + encweakness);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D9();
        }
    }
}
