using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace main
{
    internal class RSA
    {
        public ulong e = 65537; //öffentlich
        public BigInteger N; //öffentlich
        private string cypherMsg; //öffentlich

        private string msgIn; //privat
        private BigInteger p; //privat
        private BigInteger q; //privat
        private BigInteger phiN; //privat
        private BigInteger d;
        private string encryptedMsg;
        private BigInteger key;
        private BigInteger encryptedKey;
        private BigInteger decryptedKey;
        public RSA(BigInteger key)
        {
            BigInteger[] primes = set_pqMinusOneCoprimePhiN();
            this.q = primes[0];
            this.p = primes[1];
            this.key = key;
            this.N = this.q * this.p;
            this.phiN = (this.q - 1) * (this.p - 1);
            BigInteger d = EEA.extendedEA(e, this.phiN);
            if (d < 0)
            {
                d = d + this.phiN;
            }
            this.d = d;
            this.printAllAttributes();
            this.encrypt(key);
            this.decryptedKey = this.decrypt(this.encryptedKey);
        }
        public BigInteger getDecryptedMsg()
        {
            return this.decryptedKey;
        }

        /*public RSA(string msg)
        {
            BigInteger[] primes = set_pqMinusOneCoprimePhiN();
            this.q = primes[0];
            this.p = primes[1];
            this.msgIn = msg;
            this.N = this.q * this.p;
            this.phiN = (this.q-1)*(this.p-1);
            BigInteger d = EEA.extendedEA(e, this.phiN);
            if (d < 0)
            {
                d = d + this.phiN;
            }
            this.d = d;
            this.encrypt(msg);
            this.decrypt(this.encryptedMsg);
        }*/
        public RSA(string msg, ulong e, ulong p, ulong q)
        {
            this.e = e;
            this.msgIn = msg;
            this.p = p;
            this.q = q;
            try
            {
                checked
                {
                    this.N = this.q * this.p;
                }
            }
            catch (OverflowException en)
            {
                Console.WriteLine("overflow  " + en);
            }
            this.phiN = (this.q - 1) * (this.p - 1);
            BigInteger d = EEA.extendedEA(e, this.phiN);
            if (d < 0)
            {
                d = d + this.phiN;
            }
            this.d = d;
            this.printAllAttributes();
            Console.WriteLine("original message: " + msg);
            Console.WriteLine("message in Ascii: " + StringManipulation.StringManipulation.msgToASCII(msg));
            this.encrypt(msg);
            this.decrypt(this.encryptedMsg);
        }

        private string decrypt(string msg)
        {
            string decrypted;
            checked
            {
                Console.WriteLine("message thats about to be decrypted" + msg);
                BigInteger a = Crypto.Math.BigI_squareAndMultiply(StringManipulation.StringManipulation.stringToBigInt(msg), this.d, this.N);
                Console.WriteLine("a: " + a);
                string decryptedASCII = Crypto.Math.BigI_squareAndMultiply(StringManipulation.StringManipulation.stringToBigInt(msg), this.d, this.N).ToString();
                Console.WriteLine("decrypted numeric: " + decryptedASCII);
                decrypted = StringManipulation.StringManipulation.ASCIItoMsg(decryptedASCII);
            }
            Console.WriteLine("decrypted in plaintext: " + decrypted);
            return decrypted;
        }
        private string encrypt(string msg)
        {
            string encryptedMsg = "";
            Console.WriteLine(msg); Console.WriteLine(StringManipulation.StringManipulation.msgToASCII(msg));
            try
            {
                checked
                {
                    encryptedMsg = Convert.ToString(Crypto.Math.BigI_squareAndMultiply(StringManipulation.StringManipulation.stringToBigInt(StringManipulation.StringManipulation.msgToASCII(msg)), this.e, this.N));
                    this.encryptedMsg = encryptedMsg;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("encryption failed " + ex);
            }

            Console.WriteLine("ecnrypted: " + this.encryptedMsg);
            return encryptedMsg;
        }
        private BigInteger decrypt(BigInteger encryptedKey)
        {
            BigInteger decrypted = 0;
            try
            {
                checked
                {
                    decrypted = Crypto.Math.BigI_squareAndMultiply(encryptedKey, this.d, this.N);
                    Console.WriteLine("decrypted Key: " + decrypted);
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine("in decrypt was schiefgelaufen");
            }
            return decrypted;
        }
        private BigInteger encrypt(BigInteger plainKey)
        {
            BigInteger encryptedKey = 0;
            //Console.WriteLine("plain Key: " + plainKey); Console.WriteLine(StringManipulation.StringManipulation.msgToASCII(plainKey));
            try
            {
                checked
                {
                    encryptedKey = Crypto.Math.BigI_squareAndMultiply(plainKey, this.e, this.N);
                    this.encryptedKey = encryptedKey;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("encryption failed " + ex);
            }

            Console.WriteLine("ecnrypted: " + this.encryptedKey);
            return encryptedKey;
        }

        public void printAllAttributes()
        {
            Console.WriteLine("e: " + this.e);
            Console.WriteLine("p: " + this.p);
            Console.WriteLine("q: " + this.q);
            Console.WriteLine("N: " + this.N);
            Console.WriteLine("phiN: " + this.phiN);
            Console.WriteLine("d: " + this.d);
        }

        private BigInteger[] set_pqMinusOneCoprimePhiN()
        {
            //p-1 and q-1 coprime phi(N)
            BigInteger primeA = 0;
            BigInteger primeB = 0;
            BigInteger phiN;
            bool coprime = false;

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            int numberOfTasks = 4;
            Task[] tasks = new Task[numberOfTasks];

            for (int i = 0; i < numberOfTasks; i++)
            {
                tasks[i] = new Task(() => MillerRabin.PrimeProbabilistic.getPrime(), token);
                tasks[i].Start();
            }
            


            while (!coprime)
            {
                primeA = MillerRabin.PrimeProbabilistic.getPrime();
                primeB = MillerRabin.PrimeProbabilistic.getPrime();
                phiN = (primeA - 1) * (primeB - 1);
                if (Crypto.Math.gcd(phiN, primeA) == 1 && Crypto.Math.gcd(phiN, primeB) == 1)
                {
                    coprime = true;
                }
            }


            BigInteger[] primes = { primeA, primeB };
            return primes;

            //////////////////////////////////////////

            //p-1 and q-1 coprime phi(N)
            /*BigInteger primeA = 0;
            BigInteger primeB = 0;
            BigInteger phiN;
            bool coprime = false;

            while (!coprime)
            {
                primeA = MillerRabin.PrimeProbabilistic.getPrime();
                primeB = MillerRabin.PrimeProbabilistic.getPrime();
                phiN = (primeA - 1) * (primeB - 1);
                if (Crypto.Math.gcd(phiN, primeA) == 1 && Crypto.Math.gcd(phiN, primeB) == 1)
                {
                    coprime = true;
                }
            }


            BigInteger[] primes = { primeA, primeB };
            return primes;*/
        }
    }
}
