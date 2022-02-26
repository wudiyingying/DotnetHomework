using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input the arithmatic expression");
            String expressing = Console.ReadLine();
            int n = 0;
            for (int index = 0; index < expressing.Length; index++)
            {
                if (expressing[index] == '+' | expressing[index] == '/' | expressing[index] == '-' | expressing[index] == '*')
                {
                    n = index;
                }
            }

            float firstNum = float.Parse(expressing.Substring(0, n));
            float secondNum = float.Parse(expressing.Substring(n + 1));
            Console.WriteLine("result is ");
            Console.WriteLine(Calculater.calculate(firstNum, secondNum, expressing[n]));
        }
    }
    class Calculater
    {
        
        public static float calculate(float firstNum, float secondNum, char operater)
        {
            switch (operater)
            {
                case '+': return firstNum + secondNum;
                case '-': return firstNum - secondNum;
                case '*': return firstNum * secondNum;
                case '/': return firstNum / secondNum;
                default: return 0;
            }

        }
    }





}
