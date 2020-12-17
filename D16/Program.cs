using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D16
{
    public class TicketRule
    {
        public string FieldName = "";
        public int Min1 = 0;
        public int Max1 = 0;
        public int Min2 = 0;
        public int Max2 = 0;
        public int[] Counters;
    }


    class Program
    {
        static private void D16a()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D16\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            int i = 0;
            int[] myTicket;
            List<int[]> nearbyTickets = new List<int[]>();
            List<TicketRule> rules = new List<TicketRule>();

            while (i < lines.Count)
            {
                if (lines[i].Length > 0)
                {
                    if (lines[i] == "your ticket:")
                    {
                        i++;
                        myTicket = lines[i].Split(',').Select(int.Parse).ToArray();
                        i++;
                        continue;
                    }

                    if (lines[i] == "nearby tickets:")
                    {
                        i++;
                        while (i < lines.Count)
                        {
                            nearbyTickets.Add(lines[i].Split(',').Select(int.Parse).ToArray());
                            i++;
                        }
                        break;
                    }

                    string[] t = lines[i].Replace(": ", ";").Replace(" or ", ";").Replace("-", ";").Split(';');
                    rules.Add(new TicketRule() { FieldName = t[0], Min1 = Convert.ToInt32(t[1]), Max1 = Convert.ToInt32(t[2]), Min2 = Convert.ToInt32(t[3]), Max2 = Convert.ToInt32(t[4]) });
                }
                i++;
            }

            int sum = 0;
            foreach (var nt in nearbyTickets)
            {
                for (int a = 0; a < nt.Length; a++)
                {
                    bool valid = false;
                    for (int r = 0; r < rules.Count; r++)
                    {
                        if ((nt[a] >= rules[r].Min1 && nt[a] <= rules[r].Max1) || (nt[a] >= rules[r].Min2 && nt[a] <= rules[r].Max2))
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (!valid)
                        sum += nt[a];
                }
            }

            Console.WriteLine(sum);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D16b()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D16\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            int i = 0;
            int[] myTicket = new int[20];
            List<int[]> nearbyTickets = new List<int[]>();
            List<TicketRule> rules = new List<TicketRule>();

            while (i < lines.Count)
            {
                if (lines[i].Length > 0)
                {
                    if (lines[i] == "your ticket:")
                    {
                        i++;
                        myTicket = lines[i].Split(',').Select(int.Parse).ToArray();
                        i++;
                        continue;
                    }

                    if (lines[i] == "nearby tickets:")
                    {
                        i++;
                        while (i < lines.Count)
                        {
                            nearbyTickets.Add(lines[i].Split(',').Select(int.Parse).ToArray());
                            i++;
                        }
                        break;
                    }

                    string[] t = lines[i].Replace(": ", ";").Replace(" or ", ";").Replace("-", ";").Split(';');
                    rules.Add(new TicketRule() { FieldName = t[0], Min1 = Convert.ToInt32(t[1]), Max1 = Convert.ToInt32(t[2]), Min2 = Convert.ToInt32(t[3]), Max2 = Convert.ToInt32(t[4]) });
                }
                i++;
            }

            // Filter valid
            int n = 0;
            while (n < nearbyTickets.Count)
            {                
                for (int a = 0; a < nearbyTickets[n].Length; a++)
                {
                    bool valid = false;
                    for (int r = 0; r < rules.Count; r++)
                    {
                        if ((nearbyTickets[n][a] >= rules[r].Min1 && nearbyTickets[n][a] <= rules[r].Max1) || (nearbyTickets[n][a] >= rules[r].Min2 && nearbyTickets[n][a] <= rules[r].Max2))
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        nearbyTickets.RemoveAt(n);
                        n--;
                        break;
                    }
                }
                n++;
            }

            // Initialize Counters array
            rules.ForEach(r => r.Counters = new int[nearbyTickets[0].Length]);

            foreach (var nt in nearbyTickets)
            {
                for (int a = 0; a < nt.Length; a++)
                {
                    for (int r = 0; r < rules.Count; r++)
                    {
                        if ((nt[a] >= rules[r].Min1 && nt[a] <= rules[r].Max1) || (nt[a] >= rules[r].Min2 && nt[a] <= rules[r].Max2))
                            rules[r].Counters[a] += 1;
                    }
                }
            }

            for (int a = 0; a < myTicket.Length; a++)
            {
                for (int r = 0; r < rules.Count; r++)
                {
                    if ((myTicket[a] >= rules[r].Min1 && myTicket[a] <= rules[r].Max1) || (myTicket[a] >= rules[r].Min2 && myTicket[a] <= rules[r].Max2))
                        rules[r].Counters[a] += 1;
                }
            }

            int totalCount = nearbyTickets.Count + 1;
            for (int r = 0; r < rules.Count; r++)
            {
                Console.Write(r + ": ");
                for (int c = 0; c < rules[r].Counters.Length; c++)
                {
                    if (rules[r].Counters[c] == totalCount)
                        Console.Write(String.Format("{0:d2} ", c));
                }
                Console.WriteLine();
            }

            // Eyeball Mk.I
            long multi = (long)myTicket[12] * myTicket[7] * myTicket[13] * myTicket[17] * myTicket[1] * myTicket[4];
            Console.WriteLine("");
            Console.WriteLine(multi);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D16a();

            D16b();
        }
    }
}
