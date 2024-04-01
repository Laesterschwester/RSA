using System;
using System.Numerics;

namespace DiffieHellmann
{
    public class Person
    {
        private string name;
        private ulong privateExponent;
        private ulong personalKey;
        public Person(string name)
        {
            this.name = name;
        }
        public ulong getPersonalKey()
        {
            return this.personalKey;
        }
        public ulong generateKey(ulong personalK, ulong p)
        {
            return Crypto.Math.ulong_squareAndMultiply(personalK, this.personalKey, p);
        }
        public void generatePersonalKey(ulong g, ulong p)
        {
            this.personalKey = Crypto.Math.ulong_squareAndMultiply(g, choosePrivateExponent(p), p);
        }
        public ulong choosePrivateExponent(ulong p)
        {
            Random rand = new Random();
            this.privateExponent = Crypto.Math.randomUlong(1, p-1, rand);
            return this.privateExponent;
        }
        
    }
    internal class DiffieHellmann
    {
        public static ulong p;
        public static ulong g;
        public static ulong key;
        private static ulong chooseG(Random rand, ulong p) { return Crypto.Math.randomUlong(0, p, rand); }
        private static ulong chooseP(Random rand) {
            return MillerRabin.PrimeProbabilistic.getPrime(rand);
            //return 13; 
        }
        public static void start()
        {
            Random rand = new Random();

            p = chooseP(rand);
            g = chooseG(rand, p);
            
            Person Alice = new Person("Alice");
            Person Bob = new Person("Bob");
            Alice.generatePersonalKey(g, p);
            Bob.generatePersonalKey(g, p);

            //exchange of personal keys
            ulong publicKeyAlice = Alice.generateKey(Bob.getPersonalKey(), p);
            ulong publicKeyBob = Bob.generateKey(Alice.getPersonalKey(), p);
            Console.WriteLine("Bobs Key: " + publicKeyBob + "Alice key: " + publicKeyAlice);
        }
    }
}

