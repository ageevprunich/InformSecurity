using System;
using System.Security.Cryptography;
using System.Text;

namespace LW3._2
{
    class Program
    {
        public static void Main()
        {
            String answer = "po1MVkAE7IjUUwu61XxgNg==";
            String attempt = "";
            int password = 10000000;
            while (attempt != answer)
            {
                byte[] passwordAr = Encoding.Unicode.GetBytes(password.ToString());
                passwordAr = ComputeHashMd5(passwordAr);
                attempt = Convert.ToBase64String(passwordAr);
                password++;
                Console.WriteLine(password + "  " + attempt);
            }
            Console.WriteLine(password - 1 + "- password");
            static byte[] ComputeHashMd5(byte[] input)
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(input);
            }
        }
    }
}



