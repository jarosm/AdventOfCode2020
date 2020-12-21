using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RegExtract;

namespace D21
{
    class Program
    {
        private static void D21()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D21\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var foods = lines.Extract<(List<string>, List<string>)>(@"(?:(\w+) )+\(contains (?:(\w+)(?:, )?)+\)").Select(x => (new HashSet<string>(x.Item1), x.Item2)).ToArray();
            var allergens = foods.SelectMany(food => food.Item2).Distinct().ToArray();
            var candidates = new Dictionary<string, HashSet<string>>();

            foreach (var allergen in allergens)
            {
                HashSet<string> intersection = null;
                foreach (var food in foods.Where(f => f.Item2.Contains(allergen)))
                {
                    if (intersection == null)
                        intersection = new HashSet<string>(food.Item1);
                    else
                        intersection.IntersectWith(food.Item1);
                }
                candidates[allergen] = intersection;
            }

            var allCandidates = new HashSet<string>(candidates.SelectMany(c => c.Value));
            int count = 0;
            foreach (var food in foods)
            {
                count += food.Item1.Count(ingr => !allCandidates.Contains(ingr));
            }

            Console.WriteLine("Part 1: " + count);


            while (candidates.Any(c => c.Value.Count() > 1))
            {
                var singles = new HashSet<string>(candidates.Where(c => c.Value.Count() == 1).Select(c => c.Value.Single()));
                foreach (var candidate in candidates)
                {
                    if (candidate.Value.Count() > 1) candidate.Value.ExceptWith(singles);
                }
            }

            Console.WriteLine("Part 2: " + string.Join(",", candidates.OrderBy(c => c.Key).Select(c => c.Value.Single())));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D21();
        }
    }
}
