using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LW_5._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Password: ");
            string password = Console.ReadLine();
            byte[] salt = Salt.GenerateSalt();
            var hashedPassword = Salt.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);
            Console.WriteLine("Hashed Password: " + Convert.ToBase64String(hashedPassword));
        }
    }
    public class Salt
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


    private static byte[] Combine(byte[] first, byte[] second)
        {
            var comb = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, comb, 0, first.Length);
            Buffer.BlockCopy(second, 0, comb, first.Length, second.Length);
            return comb;
        }

        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed, salt));
            }
        }
    }



}