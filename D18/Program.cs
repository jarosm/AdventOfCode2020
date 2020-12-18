using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D18
{
    class Program
    {
        static private ulong EvaluatePart1(string expr, ref int index)
        {
            ulong result;

            if (expr[index] == '(')
            {
                index++;
                result = EvaluatePart1(expr, ref index);
            }
            else
            {
                result = (uint)Char.GetNumericValue(expr[index]);
                index++;
            }

            while ((index < expr.Length) && (expr[index] != ')'))
            {
                char op = expr[index];
                index++;

                ulong num; 
                if (expr[index] == '(')
                {
                    index++;
                    num = EvaluatePart1(expr, ref index);
                }
                else
                {
                    num = (uint)Char.GetNumericValue(expr[index]);
                    index++;
                }

                if (op == '+')
                    result += num;
                else
                    result *= num;
            }
            index++; // possible rigth parenthesis

            return result;
        }


        static private ulong EvaluatePart2(string expr, ref int index)
        {
            ulong result;

            if (expr[index] == '(')
            {
                index++;
                return EvaluatePart2(expr, ref index);
            }
            else
            {
                result = (uint)Char.GetNumericValue(expr[index]);
                index++;
            }

            while ((index < expr.Length) && (expr[index] != ')'))
            {
                if (expr[index] == '*')
                {
                    index++;
                    result *= EvaluatePart2(expr, ref index);
                }
                else
                {
                    index++;
                    if (expr[index] == '(')
                    {
                        index++;
                        result += EvaluatePart2(expr, ref index);
                    }
                    else
                    {
                        result += (uint)Char.GetNumericValue(expr[index]);
                        index++;
                    }
                }
            }
            if ((index < expr.Length) && (expr[index] == ')'))
                index++;

            return result;
        }


        static private void D18()
        {
            List<string> lines = new List<string>();
            using (StreamReader input = File.OpenText("d:\\programming\\Advent of Code\\data 2020\\D18\\input.txt"))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    lines.Add(line.Replace(" ", ""));
                }
            }

            ulong sum = 0;
            foreach (string expr in lines)
            {
                int index = 0;

                sum += EvaluatePart1(expr, ref index);
            }
            Console.WriteLine("Part 1: " + sum);


            Console.WriteLine("!! Part 2 gives wrong numbers !!");
            sum = 0;
            foreach (string expr in lines)
            {
                int index = 0;

                sum += EvaluatePart2(expr, ref index);
            }
            Console.WriteLine("Part 2: " + sum);

            Console.WriteLine("end");
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            D18();
        }
    }
}
