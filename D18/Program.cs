using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D18
{
    class Program
    {
        static private int GetOperatorPrecedence(char op, int part)
        {
            switch (op)
            {
                case '+':
                    return part == 1 ? 1 : 2;
                case '*':
                    return 1;
                default:
                    return 0;
            }
        }


        static private void Compute(ref Stack<long> stack, char op)
        {
            long num1 = stack.Pop(), num2 = stack.Pop();
            if (op == '+')
                stack.Push(num1 + num2);
            else
                stack.Push(num1 * num2);
        }


        static private long Evaluate(string expression, int part)
        {
            Stack<char> operatorStack = new Stack<char>();
            Stack<long> resultStack = new Stack<long>();

            foreach (char c in expression)
            {
                if (char.IsDigit(c))
                    resultStack.Push((long)char.GetNumericValue(c));
                else if (c == '+' || c == '*')
                {
                    while ((operatorStack.Count > 0) && (operatorStack.Peek() != '(') && (GetOperatorPrecedence(operatorStack.Peek(), part) >= GetOperatorPrecedence(c, part)))
                        Compute(ref resultStack, operatorStack.Pop());
                    operatorStack.Push(c);
                }
                else if (c == '(')
                    operatorStack.Push(c);
                else // ')'
                {
                    while (operatorStack.Peek() != '(')
                        Compute(ref resultStack, operatorStack.Pop());
                    operatorStack.Pop(); // '('
                }
            }
            while (operatorStack.Count > 0)
                Compute(ref resultStack, operatorStack.Pop());

            return resultStack.Pop();
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

            long sum = 0;
            foreach (string expression in lines)
            {
                sum += Evaluate(expression, 1);
            }
            Console.WriteLine("Part 1: " + sum);

            sum = 0;
            foreach (string expression in lines)
            {
                sum += Evaluate(expression, 2);
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
