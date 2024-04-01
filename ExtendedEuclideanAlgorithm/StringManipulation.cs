using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation

{
    internal class StringManipulation
    {
        public static string MsgToASCIIBinary(string msg)
        {
            string s = "";
            BigInteger bigInt = 0;
            for(int i = 0; i < msg.Length; i++)
            {
                bigInt += ((BigInteger)msg[i]) << ((msg.Length-1-i) * 7);
            }
            return Convert.ToString(bigInt);
        }
        public static string ASCIItoMsgBinary(string s_asciiString)
        {
            
            string s = "";
            BigInteger valueOfString = stringToBigInt(s_asciiString);
            BigInteger lenInBin = 0;
            BigInteger i = 1;

            while (i-1 < valueOfString)
            {
                i *= 2;               
                lenInBin++;
            }
            Console.WriteLine("length: " + lenInBin);
            for(BigInteger j = 0; j < lenInBin; j++)
            {
                BigInteger asciiValue = 0;
                for(int k = 0; k < 7; k++)
                {
                    asciiValue += (valueOfString & 1)*((ulong)Math.Pow(2, k));
                    valueOfString = valueOfString >> 1;
                }
                //Console.WriteLine("j: " + j);
                s = (char)(asciiValue)+ s;
            }
            return s;
        }
        public static string ASCIItoMsg(string s_asciiString)
        {

            char[] c_msgBuffer = new char[(int)Math.Ceiling(s_asciiString.Length/(double)3)];
            int i_msgBufferIndex = 0;

            char[] charBuffer = new char[3];
            int i_charBufferIndex = 0;

            int textIndex = 0;

            int i_asciiStringLen = s_asciiString.Length;

            int offset = (3 - (i_asciiStringLen % 3))%3;

            for (; i_charBufferIndex < offset; i_charBufferIndex++)
            {
                charBuffer[i_charBufferIndex] = '0';
            }

            for (; textIndex < i_asciiStringLen; textIndex += 3)
            {
                for (; i_charBufferIndex < 3; i_charBufferIndex++)
                {
                    charBuffer[i_charBufferIndex] = s_asciiString[i_charBufferIndex + textIndex - offset];
                }
                i_charBufferIndex = 0;
                string s = new string(charBuffer);
                c_msgBuffer[i_msgBufferIndex] = Convert.ToChar(Convert.ToInt32(s));
                i_msgBufferIndex++;
            }

            string s_msg = new string(c_msgBuffer);
            //Console.WriteLine("message in ascii: " + (int)Convert.ToChar(s_msg));
            return s_msg;
        }
        public static string msgToASCII(string msg)
        {
            string asciiString = "";
            try
            {
                checked
                {

                    char[] buffer = new char[msg.Length * 3];
                    int i_bufferIndex = 0;
                    char c;
                    string s_ascii;
                    for (int i = 0; i < msg.Length; i++)
                    {
                        c = msg[i];
                        s_ascii = Convert.ToString((int)c);
                        for (int j = 0; j < 3 - s_ascii.Length; j++)
                        {
                            buffer[i_bufferIndex] = '0';
                            i_bufferIndex++;
                        }
                        for (int j = 0; j < s_ascii.Length; j++)
                        {
                            buffer[i_bufferIndex] = s_ascii[j];
                            i_bufferIndex++;
                        }
                    }
                    asciiString = new string(buffer, 0, i_bufferIndex);
                    Console.WriteLine("string in ascii: " + asciiString);
                }
            }
            catch(Exception e) { 
                Console.WriteLine("convertierung von msg zu ascii hat fehlgeschlagen");
            }
            return asciiString;

        }
        public static BigInteger stringToBigInt(string s)
        {
            BigInteger bigInteger = new BigInteger();

            for(int i = 0; i<s.Length; i++)
            {
                bigInteger = bigInteger * 10 + (int)s[i]-48;
            }

            return bigInteger;
        }
    }
}
