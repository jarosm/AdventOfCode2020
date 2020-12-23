using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D23
{
    class Program
    {
        private class Node
        {
            public long val;
            public Node next;

            public Node(long val)
            {
                this.val = val;
            }
        }


        private static void D23a()
        {
            string text = File.ReadAllText("d:\\programming\\Advent of Code\\data 2020\\D23\\input.txt");
            List<int> cups = text.ToArray().Select(c => (int)char.GetNumericValue(c)).ToList();

            for (int i = 1; i <= 100; i++)
            {
                int first = cups[1], second = cups[2], third = cups[3];
                cups.RemoveAt(1);
                cups.RemoveAt(1);
                cups.RemoveAt(1);

                int min = cups.Min();
                int searchFor = cups[0] - 1;
                if (searchFor >= min)
                {
                    while (!cups.Contains(searchFor))
                        searchFor--;
                }
                else
                    searchFor = cups.Max();

                int index = cups.IndexOf(searchFor);
                cups.InsertRange(index + 1, new int[] { first, second, third });

                cups.Add(cups[0]);
                cups.RemoveAt(0);
            }

            while (cups[0] != 1)
            {
                cups.Add(cups[0]);
                cups.RemoveAt(0);
            }
            Console.WriteLine("Part 1: " + string.Join("", cups));
        }


        private static void D23b()
        {
            string text = File.ReadAllText("d:\\programming\\Advent of Code\\data 2020\\D23\\input.txt");
            List<int> cupsTemp = text.ToArray().Select(c => (int)char.GetNumericValue(c)).ToList();

            Node prevNode = null;
            Dictionary<long, Node> cups = new Dictionary<long, Node>();
            for (int i = 0; i < cupsTemp.Count; i++)
            {
                Node node = new Node(cupsTemp[i]);
                if (prevNode != null)
                    prevNode.next = node;
                prevNode = node;
                cups[cupsTemp[i]] = node;
            }
            for (int i = 10; i <= 1000000; i++)
            {
                Node node = new Node(i);
                prevNode.next = node;
                prevNode = node;
                cups[i] = node;
            }
            prevNode.next = cups[cupsTemp[0]];


            Node current = cups[cupsTemp[0]];
            for (long i = 0; i < 10000000; i++)
            {
                Node groupStart = current.next;
                current.next = current.next.next.next.next;

                List<long> groupVals = new List<long>() { groupStart.val, groupStart.next.val, groupStart.next.next.val };
                long nextVal = current.val == 1 ? 1000000 : current.val - 1;
                while (groupVals.Contains(nextVal))
                {
                    nextVal--;
                    if (nextVal < 1)
                        nextVal = 1000000;
                }

                Node insertAfter = cups[nextVal];
                groupStart.next.next.next = insertAfter.next;
                insertAfter.next = groupStart;
                current = current.next;
            }


            Console.WriteLine("Part 2: " + (cups[1].next.val * cups[1].next.next.val));

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D23a();

            D23b();
        }
    }
}
