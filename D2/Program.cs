using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2
{
    class Record
    {
        public int min = 0;
        public int max = 0;
        public char character = ' ';
        public string password = "";

        public Record()
        {
        }
    }


    class Program
    {
        static private void D2a()
        {
            List<Record> records = new List<Record>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D2\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    Record r = new Record();
                    string[] t1 = line.Split(' ');
                    string[] t2 = t1[0].Split('-');
                    r.min = Convert.ToInt32(t2[0]);
                    r.max = Convert.ToInt32(t2[1]);
                    r.character = t1[1][0];
                    r.password = t1[2];
                    records.Add(r);
                }
            }

            int count = 0;
            foreach (Record r in records)
            {
                int c = 0;
                for (int i = 0; i < r.password.Length; i++)
                {
                    if (r.password[i] == r.character)
                        c++;
                }
                if ((c >= r.min) && (c <= r.max))
                    count++;
            }
            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D2b()
        {
            List<Record> records = new List<Record>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D2\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    Record r = new Record();
                    string[] t1 = line.Split(' ');
                    string[] t2 = t1[0].Split('-');
                    r.min = Convert.ToInt32(t2[0]);
                    r.max = Convert.ToInt32(t2[1]);
                    r.character = t1[1][0];
                    r.password = t1[2];
                    records.Add(r);
                }
            }

            int count = 0;
            foreach (Record r in records)
            {
                if (((r.password[r.min - 1] == r.character) && (r.password[r.max - 1] != r.character))
                    || ((r.password[r.min - 1] != r.character) && (r.password[r.max - 1] == r.character)))
                {
                    count++;
                }
            }
            Console.WriteLine(count);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D2a();

            D2b();
        }
    }
}
