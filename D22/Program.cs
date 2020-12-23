using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D22
{
    class Program
    {
        private static bool CheckHistory(List<int> player, List<List<int>> history)
        {
            foreach (var combination in history)
            {
                if (combination.SequenceEqual(player))
                    return true;
            }
            return false;
        }


        private static int Game(List<int> player1, List<int> player2)
        {
            var p1History = new List<List<int>>();
            var p2History = new List<List<int>>();

            while (player1.Any() && player2.Any())
            {
                if (CheckHistory(player1, p1History) && CheckHistory(player2, p2History))
                    return 1;

                p1History.Add(player1.ToList());
                p2History.Add(player2.ToList());

                var player1Card = player1.First();
                var player2Card = player2.First();

                player2.Remove(player2Card);
                player1.Remove(player1Card);

                if (player1Card <= player1.Count && player2Card <= player2.Count)
                {
                    var winner = Game(player1.Take(player1Card).ToList(), player2.Take(player2Card).ToList());

                    if (winner == 1)
                    {
                        player1.Add(player1Card);
                        player1.Add(player2Card);

                    }
                    else
                    {
                        player2.Add(player2Card);
                        player2.Add(player1Card);
                    }
                }
                else if (player1Card > player2Card)
                {
                    player1.Add(player1Card);
                    player1.Add(player2Card);
                }
                else
                {
                    player2.Add(player2Card);
                    player2.Add(player1Card);
                }
            }

            return player1.Any() ? 1 : 2;
        }


        private static void D22a()
        {
            var text = File.ReadAllText("d:\\programming\\Advent of Code\\data 2020\\D22\\input.txt");
            var split = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var player1 = split[0].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
            var player2 = split[1].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();

            while (player1.Any() && player2.Any())
            {
                var player1Card = player1.First();
                var player2Card = player2.First();

                if (player1Card > player2Card)
                {
                    player2.Remove(player2Card);
                    player1.Remove(player1Card);

                    player1.Add(player1Card);
                    player1.Add(player2Card);
                }
                else
                {
                    player2.Remove(player2Card);
                    player1.Remove(player1Card);

                    player2.Add(player2Card);
                    player2.Add(player1Card);
                }
            }

            var winner = player1.Any() ? player1 : player2;
            var i = winner.Count;
            Console.WriteLine("Part 1: " + winner.Aggregate(0L, (a, b) => a + (b * i--)));
        }


        private static void D22b()
        {
            var text = File.ReadAllText("d:\\programming\\Advent of Code\\data 2020\\D22\\input.txt");
            var split = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var player1 = split[0].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();
            var player2 = split[1].Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToList();

            Game(player1, player2);

            var winner = player1.Any() ? player1 : player2;
            var i = winner.Count;
            Console.WriteLine("Part 2: " + winner.Aggregate(0L, (a, b) => a + (b * i--)));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D22a();

            D22b();
        }
    }
}
