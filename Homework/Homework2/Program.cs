using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("input num");
            int num = int.Parse(Console.ReadLine());
            PrimeC primeC = new PrimeC(num);
            primeC.getPrimeArray();
            Console.WriteLine("all prime factors ");
            foreach(int n in primeC.Prime)
            {
                Console.WriteLine(n);

            }*/

            int[][] test = new int[][]
            {
                new int[]{11,22,33,44},
                new int[]{11,11,22,44},
                new int[]{11,22,11,44}

            };

            ToeplitzMatrix t = new ToeplitzMatrix(test, 3, 4);
            Console.WriteLine(t.isToeplitz());


        }
    }

    class PrimeC
    {
        public int num;
        public List<int> Prime = new List<int> { };


        public PrimeC(int nu)
        {
            num = nu;
        }

        public bool isPrime(int num)
        {
            if (num == 2) return true;
            for (int i = 2; i < Math.Sqrt(num) + 1; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }


            return true;
        }
        public void getPrimeArray()
        {
            for (int i = 2; i < num; i++)
            {
                if (num % i == 0)
                {
                    if (isPrime(i)) Prime.Add(i);
                }
            }
        }
    }

    class CalcuArray
    {

        public int[] intArray;

        public CalcuArray(int[] intArrayC)
        {
            intArray = intArrayC;
        }

        public int getMaxNum()
        {
            int MaxNum = intArray[0];
            foreach (int n in intArray)
            {
                if (MaxNum < n) MaxNum = n;
            }

            return MaxNum;
        }
        public int getMinNum()
        {
            int MinNum = intArray[0];
            foreach (int n in intArray)
            {
                if (MinNum > n) MinNum = n;
            }

            return MinNum;
        }

        public float getSum()
        {
            float sum = 0;
            foreach (int n in intArray)
            {
                sum += n;
            }
            return sum;
        }

        public float getAverage()
        {
            return getSum() / intArray.Length;
        }

    }

    class AiCalculator
    {
        public List<int> prime = new List<int> { };

        AiCalculator()
        {
            for (int i = 2; i <= 100; i++)
            {
                prime.Add(i);
            }

            for (int n = 2; n <= 10; n++)
            {
                for (int j = n; j <= 100; j *= j)
                {
                    prime.Remove(j);
                }
            }
            prime.Add(2);
        }

    }

    class ToeplitzMatrix
    {
        int[][] nu;
        int col;
        int row;
        public ToeplitzMatrix(int[][] nuC, int colC, int rowC)
        {
            nu = nuC;
            col = colC;
            row = rowC;

        }

        public bool isToeplitz()
        {
            int[] num = new int[Math.Abs(col - row) + 1];

            for (int i = 0; i < num.Length; i++)
            {
                num[i] = nu[0][i];
            }

            for (int i = 0; i < num.Length; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (num[i] != nu[j][j + i]) return false;


                }


            }
            return true;
        }
    }
}
