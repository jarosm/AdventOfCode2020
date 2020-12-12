using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D12
{
    class Program
    {
        static private void D12a()
        {
            int northPos = 0, eastPos = 0, direction = 90;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D12\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    char action = line[0];
                    int value = Convert.ToInt32(line.Substring(1));

                    if (action == 'F')
                    {
                        switch (direction)
                        {
                            case 0:
                                action = 'N';
                                break;
                            case 90:
                                action = 'E';
                                break;
                            case 180:
                                action = 'S';
                                break;
                            case 270:
                                action = 'W';
                                break;
                        }
                    }

                    switch (action)
                    {
                        case 'R':
                            direction = (direction + value) % 360;
                            break;
                        case 'L':
                            direction = (360 + direction - value) % 360;
                            break;
                        case 'N':
                            northPos += value;
                            break;
                        case 'S':
                            northPos -= value;
                            break;
                        case 'E':
                            eastPos += value;
                            break;
                        case 'W':
                            eastPos -= value;
                            break;
                    }
                }
            }

            Console.WriteLine(Math.Abs(northPos) + Math.Abs(eastPos));

            Console.WriteLine("end");
            Console.ReadLine();
        }

        static private void D12b()
        {
            int northPos = 0, eastPos = 0, northPosWaypoint = 1, eastPosWaypoint = 10;

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D12\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    char action = line[0];
                    int value = Convert.ToInt32(line.Substring(1));

                    switch (action)
                    {
                        case 'F':
                            northPos += northPosWaypoint * value;
                            eastPos += eastPosWaypoint * value;
                            break;
                        case 'R':
                            while (value > 0)
                            {
                                int t1 = northPosWaypoint;
                                northPosWaypoint = -1 * eastPosWaypoint;
                                eastPosWaypoint = t1;
                                value -= 90;
                            }
                            break;
                        case 'L':
                            while (value > 0)
                            {
                                int t2 = northPosWaypoint;
                                northPosWaypoint = eastPosWaypoint;
                                eastPosWaypoint = -1 * t2;
                                value -= 90;
                            }
                            break;
                        case 'N':
                            northPosWaypoint += value;
                            break;
                        case 'S':
                            northPosWaypoint -= value;
                            break;
                        case 'E':
                            eastPosWaypoint += value;
                            break;
                        case 'W':
                            eastPosWaypoint -= value;
                            break;
                    }
                }
            }

            Console.WriteLine(Math.Abs(northPos) + Math.Abs(eastPos));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D12a();

            D12b();
        }
    }
}
