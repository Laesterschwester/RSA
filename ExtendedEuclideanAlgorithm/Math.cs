using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace Crypto
{
    internal class Math
    {
        public static int gcd(int a, int b)
        {
            int c = a / b;
            int rest = a % b;

            if (rest == 0)
            {
                return b;
            }

            a = b;
            b = rest;
            return gcd(a, b);
        }
        public static int gcd(ulong a, ulong b)
        {
            ulong c = a / b;
            ulong rest = a % b;

            if (rest == 0)
            {
                return (int)b;
            }

            a = b;
            b = rest;
            return gcd(a, b);
        }
        public static BigInteger gcd(BigInteger a, BigInteger b)
        {
            BigInteger c = a / b;
            BigInteger rest = a % b;

            if (rest == 0)
            {
                return b;
            }

            a = b;
            b = rest;
            return gcd(a, b);
        }
        public static BigInteger BigI_squareAndMultiply(BigInteger b, BigInteger exponent, BigInteger mod)
        {


            BigInteger result = 1;
            int l = 1;

            for (BigInteger i = 2; i <= exponent; l++)
            {
                i = i * 2;
            }

            for (int j = l; j > 0; j--)
            {
                if ((1 & exponent >> j - 1) == 0)
                {
                    result = (result * result) % mod;
                }
                else
                {
                    result = (result * result) % mod;
                    result = (b * result) % mod;
                }                
            }
            return result;

        }
        public static ulong ulong_squareAndMultiply(ulong b, ulong exponent, ulong mod)
        {
            checked
            {
                BigInteger result = 1;
                int l = 1;

                for (BigInteger i = 2; i <= exponent; l++)
                {
                    i = i * 2;
                }

                for (int j = l; j > 0; j--)
                {
                    if ((1 & exponent >> j - 1) == 0)
                    {
                        result = (result * result) % mod;
                    }
                    else
                    {
                        result = (result * result) % mod;
                        result = (b * result) % mod;
                    }

                }
                return (ulong)result;
            }
        }
        public static int modularExponentiation(int b, int exponent, int mod)
        {
            checked
            {
                int c = 1;
                int eNew = 1;
                c = (b * c) % mod;
                for (; eNew < exponent; eNew++)
                {
                    try
                    {
                        checked
                        {
                            c = (b * c) % mod;
                        }
                    }

                    catch (OverflowException ex)
                    {
                        Console.WriteLine("Integer overflow detected: " + ex.Message);
                    }
                }
                return c;
            }
        }
        public static BigInteger _1024BitCryptographicallyRandomNumber()
        {
            int size = 128;
            byte[] byteArray = new byte[size];
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(byteArray);

            /*for(int i = 0; i < byteArray.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write((byteArray[i] >> (7 - j)) & 1);
                }
            }*/

            BigInteger bigInt = 0;
            for (int i = 0; i < size; i++)
            {
                BigInteger tmp = (BigInteger)byteArray[i];
                bigInt += tmp << ((size - 1 - i) * 8);
            }

            return bigInt;
        }
        public static ulong randomUlong(ulong lowerBound, ulong upperBound, Random rnd)
        {
            byte[] buffer = new byte[8];
            ulong range = upperBound - lowerBound + 1;
            rnd.NextBytes(buffer);
            ulong randomUlong = BitConverter.ToUInt64(buffer, 0) % range + lowerBound;
            return randomUlong;
        }

        public static ulong modularExponentiationBigInt(ulong b, ulong exponent, ulong mod)
        {
            checked
            {
                BigInteger basis = b;
                BigInteger c = 1;
                ulong eNew = 1;
                c = (basis * c) % mod;
                for (; eNew < exponent; eNew++)
                {
                    try
                    {
                        checked
                        {
                            c = (basis * c) % mod;
                        }
                    }

                    catch (OverflowException ex)
                    {
                        //Console.WriteLine("Integer overflow detected: " + ex.Message);
                        Console.WriteLine("Overflow :/ " + ex);
                    }
                }
                return (ulong)c;
            }
        }
        public static ulong modularExponentiationBigInt(BigInteger b, ulong exponent, ulong mod)
        {
            checked
            {
                BigInteger basis = b;
                BigInteger c = 1;
                ulong eNew = 1;
                c = (basis * c) % mod;
                for (; eNew < exponent; eNew++)
                {
                    try
                    {
                        checked
                        {
                            c = (basis * c) % mod;
                        }
                    }

                    catch (OverflowException ex)
                    {
                        //Console.WriteLine("Integer overflow detected: " + ex.Message);
                        Console.WriteLine("Overflow :/ " + ex);
                    }
                }
                return (ulong)c;
            }
        }
        public static int modularExponentiationBigInt(int b, int r, int d, int mod)
        {
            checked
            {
                BigInteger e = d << r;
                int c = 1;
                int eNew = 1;
                c = (b * c) % mod;
                for (; eNew < e; eNew++)
                {
                    c = (b * c) % mod;
                }
                return c;
            }
        }
    }
}
