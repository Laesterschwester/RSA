using MillerRabin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace main
{
    internal class main
    {
        public static TimeSpan timer;
        public static void Main(string[] args) {
            try
            {
                checked
                {
                    /*string msg;
                    msg = Console.ReadLine();
                    string asciiBin = StringManipulation.StringManipulation.MsgToASCIIBinary(msg);
                    Console.WriteLine("start: " + asciiBin);
                    string msgFromAscii = StringManipulation.StringManipulation.ASCIItoMsgBinary(asciiBin);
                    Console.WriteLine("end: " + msgFromAscii);*/

                    //DiffieHellmann.DiffieHellmann.start();

                    //18446744073709551615
                    
                    Console.WriteLine("enter Messgage");
                    string msg = Console.ReadLine();
                    BigInteger key = StringManipulation.StringManipulation.stringToBigInt(StringManipulation.StringManipulation.MsgToASCIIBinary(msg));
                    RSA rsa = new RSA(key);
                    Console.WriteLine("decrypted keyasdfasdfasdf: " + rsa.getDecryptedMsg());
                    Console.WriteLine(StringManipulation.StringManipulation.ASCIItoMsgBinary(Convert.ToString(rsa.getDecryptedMsg())));

                    //rsa.printAllAttributes();

                    //RSA rsa = new RSA(msg, 23, 5911263269, 8517832913);

                    //RSA rsa = new RSA(msg, 23, 5911263269, 8517832913);                    
                }
            }
            catch(OverflowException) {
                Console.WriteLine("shite");
            }
        }
    }
}
