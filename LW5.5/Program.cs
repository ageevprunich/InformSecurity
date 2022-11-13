using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

namespace lW_5._5
{
    internal class Program
    {
        static void Main(string[] args)
        {


        Console.Write("What do you want to do?: " +
            "\n1 - login" +
            "\n2 - registration \n" );
            char comand = Convert.ToChar(Console.ReadLine());

            string path = "..\\..\\..\\.\\users\\";
            string ext_txt = ".txt";
            string tofilepassword = "_pasword";
            string tofilesalt = "_salt";

            string login;
            string password;
            string path_to_salt;
            string path_to_password;
            string hashpassword;
            byte[] bsalt = new byte[32];
            string filedir;
            var timer = new Stopwatch();

            switch (comand)
            {
                case '1':
                    Console.Write("Login: ");
                    login = Console.ReadLine();

                    filedir = path + login + "\\";
                    path_to_password = filedir + login + tofilepassword + ext_txt;
                    path_to_salt = filedir + login + tofilesalt + ext_txt;

                    Console.Write("Password: ");
                    password = Console.ReadLine();

                    try
                    {
                        timer.Start();
                        using (StreamReader read = File.OpenText(path_to_password))
                        {
                            hashpassword = read.ReadLine();
                        }

                        using (var read = new FileStream(path_to_salt, FileMode.Open))
                        {
                            read.Read(bsalt, 0, bsalt.Length);
                        }
                        password = Convert.ToBase64String(PBKDF2.HashPassword(password, bsalt, 210000, HashAlgorithmName.SHA512));
                        timer.Stop();
                        if (password == hashpassword)
                        {
                            Console.WriteLine("\nSuccessful\n" +
                                "Time: " + timer.ElapsedMilliseconds + "ms");
                        }
                        else
                        {
                            Console.WriteLine("\nSomething wrong....");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nUser not found....");
                    }
                    break;


                case '2':
                    Console.Write("Login: ");
                    login = Console.ReadLine();
                    Console.Write("Password: ");
                    password = Console.ReadLine();

                    timer.Start();
                    bsalt = PBKDF2.GenerateSalt();
                    password = Convert.ToBase64String(PBKDF2.HashPassword(password, bsalt, 210000, HashAlgorithmName.SHA512));
                    filedir = path + login + "\\";
                    Directory.CreateDirectory(filedir);
                    path_to_password = filedir + login + tofilepassword + ext_txt;
                    path_to_salt = filedir + login + tofilesalt + ext_txt;

                    try
                    {

                        using (var stream = new FileStream(path_to_salt, FileMode.Create))
                        {
                            stream.Write(bsalt, 0, bsalt.Length);
                        };

                        using (StreamWriter write = new StreamWriter(path_to_password))
                        {
                            write.WriteLine(password);
                        };
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    timer.Stop();
                    Console.WriteLine("\nFinish\n" +
                        "Time: " + timer.ElapsedMilliseconds + "ms");
                    break;
            }
        }
    }

    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var rndnumgen = new RNGCryptoServiceProvider())
            {
                var rndnum = new byte[32];
                rndnumgen.GetBytes(rndnum);
                return rndnum;
            }
        }
        public static byte[] HashPassword(string toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName alg)
        {

            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, alg))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }


}