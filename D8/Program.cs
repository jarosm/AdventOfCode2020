using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D8
{
    class Instruction
    {
        public string Operation;
        public int Value;
        public bool Visited;

        public Instruction()
        {
            Operation = "";
            Value = 0;
            Visited = false;
        }
    }


    class Program
    {
        static List<Instruction> program = new List<Instruction>();
        static Int64 accumulator = 0;


        static private bool Execute()
        {
            accumulator = 0;
            int index = 0;
            while (index < program.Count)
            {
                if (program[index].Visited)
                    break;

                program[index].Visited = true;
                switch (program[index].Operation)
                {
                    case "acc":
                        accumulator += program[index].Value;
                        index++;
                        break;
                    case "jmp":
                        index += program[index].Value;
                        break;
                    case "nop":
                        index++;
                        break;
                }
            }
            if (index < program.Count)
                return false;
            else
                return true;
        }


        static private void D8a()
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D8\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] t1 = line.Split(' ');
                    program.Add(new Instruction() { Operation = t1[0], Value = Convert.ToInt32(t1[1]) });
                }
            }

            Execute();

            Console.WriteLine(accumulator);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static private void D8b()
        {
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D8\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] t1 = line.Split(' ');
                    program.Add(new Instruction() { Operation = t1[0], Value = Convert.ToInt32(t1[1]) });
                }
            }

            for (int i = 0; i < program.Count(); i++)
            {
                if (program[i].Operation == "acc")
                    continue;

                for (int k = 0; k < program.Count(); k++)
                    program[k].Visited = false;

                if (program[i].Operation == "nop")
                    program[i].Operation = "jmp";
                else
                    program[i].Operation = "nop";

                if (Execute())
                    break;

                if (program[i].Operation == "nop")
                    program[i].Operation = "jmp";
                else
                    program[i].Operation = "nop";
            }

            Console.WriteLine(accumulator);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            //D8a();

            D8b();
        }
    }
}
