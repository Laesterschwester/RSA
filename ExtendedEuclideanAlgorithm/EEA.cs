using System;
using System.Collections.Generic;
using System.Numerics;

namespace main
{
    internal class EEA
    {
        /*
        public static void start(string[] args)
        {
            Console.WriteLine("k: " + extendedEA(23, 120));
        }
        */

        private static void printList(List<int[]> list)
        {
            int listLen = list.Count;
            for(int i = 0; i<listLen; i++)
            {
                for(int j = 0; j<4; j++)
                {
                    Console.Write(list[i][j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
        public static int euclideanAlgorithm(int a, int b)
        {
            //a = b*c + rest
            int c = a / b;
            int rest = a % b;


            if (rest == 0)
            {
                return b;
            }

            a = b;
            b = rest;
            return euclideanAlgorithm(a, b);
        }
        public static ulong euclideanAlgorithm(ulong a, ulong b, List<ulong[]> list)
        {
            //a = b*c + rest
            ulong c = a / b;
            ulong rest = a % b;

            if (rest == 0)
            {
                return b;
            }

            //dahinter, damit rest 0 nicht dabei ist
            ulong[] arr = { a, b, c, rest };
            list.Add(arr);

            a = b;
            b = rest;
            return euclideanAlgorithm(a, b, list);
        }
        public static BigInteger euclideanAlgorithm(BigInteger a, BigInteger b, List<BigInteger[]> list)
        {
            //a = b*c + rest
            BigInteger c = a / b;
            BigInteger rest = a % b;

            if (rest == 0)
            {
                return b;
            }

            //dahinter, damit rest 0 nicht dabei ist
            BigInteger[] arr = { a, b, c, rest };
            list.Add(arr);

            a = b;
            b = rest;
            return euclideanAlgorithm(a, b, list);
        }
        public static long extendedEA(ulong a, ulong b)
        {
            List<ulong[]> list = new List<ulong[]>();
            euclideanAlgorithm(a, b, list);
            //printList(list);

            int listLen = list.Count;
            long result; //result = d in  e*d = 1 mod phi(n)

            long d = 1; // gcd = b*k + c*j
            long k = -(long)list[listLen - 1][2];
            long tmp;
            //Console.WriteLine(k + " "+  j);
            for(int i = listLen - 2; i >= 0; i--)
            {
                tmp = k;
                k = k * -(long)list[i][2] + d;
                d = tmp;
                //Console.WriteLine(k + " " + j);
            }
            result = d;
            return result;
        }
        public static BigInteger extendedEA(BigInteger a, BigInteger b)
        {
            List<BigInteger[]> list = new List<BigInteger[]>();
            euclideanAlgorithm(a, b, list);
            //printList(list);

            int listLen = list.Count;
            BigInteger result; //result = d in  e*d = 1 mod phi(n)

            BigInteger d = 1; // gcd = b*k + c*j
            BigInteger k = -list[listLen - 1][2];
            BigInteger tmp;
            //Console.WriteLine(k + " "+  j);
            for (int i = listLen - 2; i >= 0; i--)
            {
                tmp = k;
                k = k * -list[i][2] + d;
                d = tmp;
                //Console.WriteLine(k + " " + j);
            }
            result = d;
            return result;
        }
    }
}
