using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D25
{
    class Program
    {
        private static void D25a()
        {
            var text = File.ReadAllText("d:\\programming\\Advent of Code\\data 2020\\D25\\input.txt");
            var publicKeys = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray();

            int[] loops = new int[2] { 0, 0 };
            ulong subjectNumber = 7;
            ulong modNumber = 20201227;

            ulong number = 1;
            while (number != publicKeys[0])
            {
                loops[0]++;
                number *= subjectNumber;
                number %= modNumber;
            }
            number = 1;
            while (number != publicKeys[1])
            {
                loops[1]++;
                number *= subjectNumber;
                number %= modNumber;
            }

            number = 1;
            subjectNumber = publicKeys[1];
            for (int i = 0; i < loops[0]; i++)
            {
                number *= subjectNumber;
                number %= modNumber;
            }
            Console.WriteLine("Part 1: " + number);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D25a();
        }
    }
}
