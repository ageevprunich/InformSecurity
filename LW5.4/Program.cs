using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace LW_5._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Write password: ");
            string password = Console.ReadLine();

            Console.Write("Choose algoritm (1 - SHA1 2 - SHA256 3 - SHA384 4 - SHA512)");
            int alg_chs = Convert.ToInt32(Console.ReadLine());

            HashAlgorithmName alg = HashAlgorithmName.SHA1;
            if (alg_chs == 1)
                alg = HashAlgorithmName.SHA1;
            if (alg_chs == 2)
                alg = HashAlgorithmName.SHA256;
            if (alg_chs == 3)
                alg = HashAlgorithmName.SHA384;
            if (alg_chs == 4)
                alg = HashAlgorithmName.SHA512;

            int step = 50000;
            int count = 0;
            byte[] salt;
            int first = 21 * 10000;

            while (count != 10)
            {
                var timer = new Stopwatch();
                salt = PBKDF2.GenerateSalt();
                timer.Start();
                string hashedPassword = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), salt, first + (count * step), alg));
                timer.Stop();

                Console.WriteLine("Hashed password: " + hashedPassword + "    Salt: " + Convert.ToBase64String(salt) +
                    "     Iterations: " + (first + (count * step)) + "    Time: " + timer.ElapsedMilliseconds);
                count++;
                timer.Reset();
            }

        }

    }

    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var rndnumgen = new RNGCryptoServiceProvider())
            {
                var rnd = new byte[32];
                rndnumgen.GetBytes(rnd);
                return rnd;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName alg)
        {

            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, alg))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }

}