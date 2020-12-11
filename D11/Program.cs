using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D11
{
    class Program
    {
        static private int CountFullSeats(List<string> seats)
        {
            int res = 0;
            foreach (string s in seats)
                res += s.Count(c => c == '#');
            return res;
        }


        static private void D11a()
        {
            List<string> seats = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D11\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    seats.Add("." + line + ".");
                }
                line = "";
                for (int i = 0; i < seats[0].Length; i++)
                    line += ".";
                seats.Insert(0, line);
                seats.Add(line);
            }

            int fullseatcount = int.MinValue, count = 0, imaxcol = seats[0].Length - 1, imaxrow = seats.Count() - 1;
            while (count != fullseatcount)
            {
                fullseatcount = count;

                List<string> temp = new List<string>();
                for (int r = 0; r <= imaxrow; r++)
                {
                    string ts = "";
                    for (int c = 0; c <= imaxcol; c++)
                    {
                        if ((r == 0) || (r == imaxrow) || (c == 0) || (c == imaxcol))
                        {
                            ts += ".";
                            continue;
                        }

                        int occupied = (seats[r - 1][c - 1] == '#' ? 1 : 0) + (seats[r - 1][c] == '#' ? 1 : 0) + (seats[r - 1][c + 1] == '#' ? 1 : 0)
                            + (seats[r][c - 1] == '#' ? 1 : 0) + (seats[r][c + 1] == '#' ? 1 : 0)
                            + (seats[r + 1][c - 1] == '#' ? 1 : 0) + (seats[r + 1][c] == '#' ? 1 : 0) + (seats[r + 1][c + 1] == '#' ? 1 : 0);

                        if ((seats[r][c] == 'L') && (occupied == 0))
                            ts += "#";
                        else
                        {
                            if ((seats[r][c] == '#') && (occupied >= 4))
                                ts += "L";
                            else
                                ts += seats[r][c];
                        }
                    }
                    temp.Add(ts);
                }

                seats = temp;
                count = CountFullSeats(seats);
            }

            Console.WriteLine("Part 1: " + fullseatcount);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D11b()
        {
            List<string> seats = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D11\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    seats.Add("." + line + ".");
                }
                line = "";
                for (int i = 0; i < seats[0].Length; i++)
                    line += ".";
                seats.Insert(0, line);
                seats.Add(line);
            }

            int fullseatcount = int.MinValue, count = 0, imaxcol = seats[0].Length - 1, imaxrow = seats.Count() - 1;
            while (count != fullseatcount)
            {
                fullseatcount = count;

                List<string> temp = new List<string>();
                for (int r = 0; r <= imaxrow; r++)
                {
                    string ts = "";
                    for (int c = 0; c <= imaxcol; c++)
                    {
                        if ((r == 0) || (r == imaxrow) || (c == 0) || (c == imaxcol))
                        {
                            ts += ".";
                            continue;
                        }


                        int occupied = 0;

                        int rt = r - 1, ct = c - 1;
                        while (rt > 0 && ct > 0)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            rt--;
                            ct--;
                        }
                        rt = r - 1; ct = c;
                        while (rt > 0)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            rt--;
                        }
                        rt = r - 1; ct = c + 1;
                        while (rt > 0 && ct < imaxcol)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            rt--;
                            ct++;
                        }
                        rt = r; ct = c - 1;
                        while (ct > 0)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            ct--;
                        }
                        rt = r; ct = c + 1;
                        while (ct < imaxcol)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            ct++;
                        }
                        rt = r + 1; ct = c - 1;
                        while (rt < imaxrow && ct > 0)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            rt++;
                            ct--;
                        }
                        rt = r + 1; ct = c;
                        while (rt < imaxrow)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            rt++;
                        }
                        rt = r + 1; ct = c + 1;
                        while (rt < imaxrow && ct < imaxcol)
                        {
                            if (seats[rt][ct] == '#')
                                occupied++;
                            if (seats[rt][ct] == '#' || seats[rt][ct] == 'L')
                                break;
                            rt++;
                            ct++;
                        }


                        if ((seats[r][c] == 'L') && (occupied == 0))
                            ts += "#";
                        else
                        {
                            if ((seats[r][c] == '#') && (occupied >= 5))
                                ts += "L";
                            else
                                ts += seats[r][c];
                        }
                    }
                    temp.Add(ts);
                }

                seats = temp;
                count = CountFullSeats(seats);
            }

            Console.WriteLine("Part 2: " + fullseatcount);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D11a();

            D11b();
        }
    }
}
