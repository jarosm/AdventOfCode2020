using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D7
{
    class Program
    {
        static List<string> countedBags = new List<string>();
        static int CountOutermostBags(string name, Dictionary<string, List<string>> rules)
        {
            int count = 0;

            if (rules.ContainsKey(name))
            {
                List<string> parents = rules[name];
                for (int i = 0; i < parents.Count; i++)
                {
                    if (!countedBags.Contains(parents[i]))
                    {
                        countedBags.Add(parents[i]);
                        count++;
                        count += CountOutermostBags(parents[i], rules);
                    }
                }
            }

            return count;
        }


        static int CountInnerBags(string name, Dictionary<string, List<(int, string)>> rules)
        {
            int count = 0;

            if (rules.ContainsKey(name))
            {
                List<(int, string)> childs = rules[name];
                for (int i = 0; i < childs.Count; i++)
                {
                    count += childs[i].Item1;
                    count += childs[i].Item1 * CountInnerBags(childs[i].Item2, rules);
                }
            }

            return count;
        }


        static private void D7a()
        {
            Dictionary<string, List<string>> rules = new Dictionary<string, List<string>>();

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D7\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] t1 = line.Replace(" bags contain ", ";").Replace(" bags, ", ";").Replace(" bag, ", ";").Replace(" bags.", "").Replace(" bag.", "").Split(';');
                    if (t1[1] == "no other")
                        continue;

                    for (int i = 1; i < t1.Length; i++)
                    {
                        string[] t2 = t1[i].Split(' ');
                        string name = t2[1] + " " + t2[2];

                        if (!rules.ContainsKey(name))
                            rules.Add(name, new List<string>());
                        rules[name].Add(t1[0]);
                    }
                }
            }

            Console.WriteLine(CountOutermostBags("shiny gold", rules));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D7b()
        {
            Dictionary<string, List<(int, string)>> rules = new Dictionary<string, List<(int, string)>>();

            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D7\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] t1 = line.Replace(" bags contain ", ";").Replace(" bags, ", ";").Replace(" bag, ", ";").Replace(" bags.", "").Replace(" bag.", "").Split(';');
                    if (t1[1] == "no other")
                        continue;

                    if (!rules.ContainsKey(t1[0]))
                        rules.Add(t1[0], new List<(int, string)>());

                    for (int i = 1; i < t1.Length; i++)
                    {
                        string[] t2 = t1[i].Split(' ');

                        rules[t1[0]].Add((Convert.ToInt32(t2[0]), t2[1] + " " + t2[2]));
                    }
                }
            }

            Console.WriteLine(CountInnerBags("shiny gold", rules));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D7a();

            D7b();
        }
    }
}
