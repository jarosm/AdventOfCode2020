using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D19
{
    public class UnprocessedRule
    {
        public int ID;
        public List<int> Part1;
        public List<int> Part2;

        public UnprocessedRule()
        {
            ID = -1;
            Part1 = new List<int>();
            Part2 = new List<int>();
        }
    }


    class Program
    {
        static private List<string> ProcessRules(List<int> unprocessedRules, Dictionary<int, List<string>> processedRules)
        {
            List<string> oldRuleset = new List<string>();
            oldRuleset.Add("");

            foreach (int id in unprocessedRules)
            {
                List<string> rules = processedRules[id];
                List<string> newRuleset = new List<string>();

                foreach (string s1 in oldRuleset)
                {
                    foreach (string s2 in rules)
                        newRuleset.Add(s1 + s2);
                }

                oldRuleset = newRuleset;
            }

            return oldRuleset;
        }


        static private void D19()
        {
            Dictionary<int, List<string>> processedRules = new Dictionary<int, List<string>>();
            List<UnprocessedRule> unprocessedRules = new List<UnprocessedRule>();
            List<string> lines = new List<string>();

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D19\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    if (line.Contains(':')) // rule
                    {
                        if (line.Contains('"')) // basic rule
                        {
                            List<string> rules = new List<string>();
                            rules.Add(line.Split('"')[1]);
                            processedRules[Convert.ToInt32(line.Split(':')[0])] = rules;
                            continue;
                        }

                        string[] t1 = line.Replace(": ", ":").Replace(" | ", "|").Split(':');
                        UnprocessedRule rule = new UnprocessedRule();
                        rule.ID = Convert.ToInt32(t1[0]);
                        string[] t2 = t1[1].Split('|');
                        string[] t3 = t2[0].Split(' ');
                        foreach (string s in t3)
                            rule.Part1.Add(Convert.ToInt32(s));
                        if (t2.Length > 1)
                        {
                            t3 = t2[1].Split(' ');
                            foreach (string s in t3)
                                rule.Part2.Add(Convert.ToInt32(s));
                        }
                        unprocessedRules.Add(rule);
                    }
                    else
                    {
                        if (line.Length > 0)
                            lines.Add(line);
                    }
                }
            }

            // Process rules
            int index = 0;
            while (unprocessedRules.Count > 0)
            {
                bool allKeysInProcessed = true;
                foreach (int key in unprocessedRules[index].Part1)
                {
                    if (!processedRules.ContainsKey(key))
                    {
                        allKeysInProcessed = false;
                        break;
                    }
                }

                if (allKeysInProcessed && unprocessedRules[index].Part2.Count > 0)
                {
                    foreach (int key in unprocessedRules[index].Part2)
                    {
                        if (!processedRules.ContainsKey(key))
                        {
                            allKeysInProcessed = false;
                            break;
                        }
                    }
                }

                if (!allKeysInProcessed)
                {
                    index++;
                    continue;
                }

                List<string> rules = ProcessRules(unprocessedRules[index].Part1, processedRules);
                if (unprocessedRules[index].Part2.Count > 0)
                    rules.AddRange(ProcessRules(unprocessedRules[index].Part2, processedRules));

                processedRules[unprocessedRules[index].ID] = rules;
                unprocessedRules.RemoveAt(index);
                index = 0;
            }

            // Count matches
            int sum = 0;
            List<string> rules0 = processedRules[0];
            foreach (string s in lines)
            {
                if (rules0.Contains(s))
                    sum++;
            }
            Console.WriteLine("Part 1: " + sum);

            sum = 0;
            List<string> rules42 = processedRules[42];
            List<string> rules31 = processedRules[31];
            foreach (string s in lines)
            {
                int count42 = 0, count31 = 0;
                string subStr = s;

                int i = rules42.FindIndex(a => subStr.StartsWith(a));
                while (i >= 0)
                {
                    subStr = subStr.Substring(rules42[i].Length);
                    count42++;
                    i = rules42.FindIndex(a => subStr.StartsWith(a));
                }
                if (count42 < 2)
                    continue;

                i = rules31.FindIndex(a => subStr.StartsWith(a));
                while (i >= 0)
                {
                    subStr = subStr.Substring(rules31[i].Length);
                    count31++;
                    i = rules31.FindIndex(a => subStr.StartsWith(a));
                }
                if ((count31 < 1) || (subStr.Length > 0) || (count42 - count31 < 1))
                    continue;

                sum++;
            }
            Console.WriteLine("Part 2: " + sum);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D19();
        }
    }
}
