using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace LW3._4
{
    class Program
    {
        static void Main(string[] args)
        {
            int amount = 32;   
            string login; //логін
            string password; //пароль
            string loginCheck; //перевірка логіну
            string passwordCheck; //перевірка паролю
            byte[] key, passwordInArray; //довжина солі

            Console.Write("Username: ");
            login = Console.ReadLine();                          
            Console.Write("Password: ");
            password = Console.ReadLine();                         
            passwordInArray = Encoding.Unicode.GetBytes(password);  


            key = cryptoKey(amount); //генерація випадкового ключа

            
            var Hmac = ComputeHmac(passwordInArray, key); //хешування паролю
            Console.WriteLine("SHA256:  " + Convert.ToBase64String(Hmac));
            Console.WriteLine();


            Console.Write("Username: ");
            loginCheck = Console.ReadLine();               
            Console.Write("Password: ");
            passwordCheck = Console.ReadLine();               
            var bpasswordCheck = Encoding.Unicode.GetBytes(passwordCheck);//запис пароля у вигляді масива 
            var HmacCheck = ComputeHmac(bpasswordCheck, key); //хешування пароля для перевірки
            Console.WriteLine("SHA256: " + Convert.ToBase64String(HmacCheck));
            Console.WriteLine();

            //перевірка
            Checkpass(Hmac, HmacCheck);

            static int Checkpass(byte[] hash1, byte[] hash2)
            {
                if (Convert.ToBase64String(hash1) == Convert.ToBase64String(hash2))
                {
                    Console.WriteLine("\nWelcome, you authorized successfuly");
                }
                else
                {
                    Console.WriteLine("\nSomething wrong");
                }
                return 1;
            }

            static byte[] cryptoKey(int amount)
            {
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    var Key = new byte[amount];
                    rng.GetBytes(Key);
                    return Key;
                }
            }

            static byte[] ComputeHmac(byte[] toBeHashed, byte[] key)
            {
                using (var hmac = new HMACSHA256(key))
                {
                    return hmac.ComputeHash(toBeHashed);
                }
            }
        }
    }
}