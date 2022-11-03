using System;
using System.Text;
using System.Security.Cryptography;

Console.Write("Write message: ");
string strForHash = Console.ReadLine();
var bstrForHash = Encoding.Unicode.GetBytes(strForHash);

const int keySize = 32;
static byte[] GenerateKey()
{
    using (var randomNumberGenerator =
    new RNGCryptoServiceProvider())
    {
        var randomNumber = new byte[keySize] ;
        randomNumberGenerator.GetBytes(randomNumber);
        return randomNumber;
    }
}

var key = GenerateKey();

Console.Write("\nAlgoritm to use: " +
    "\n1 - hmacmd5 " +
    "\n2 - hmacsha256 " +
    "\n3 - hmacsha512 " );

char choose = Convert.ToChar(Console.ReadLine());

switch (choose)
{
    case '1':
        var md5hmacForStr = hash.ComputeHash(bstrForHash, "hmacmd5", key);
        Console.WriteLine("\nHash MD5 HMAC: " + (Convert.ToBase64String(md5hmacForStr)));
        break;


    case '2':
        var sha256hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha256", key);
        Console.WriteLine("\nHash SHA256 HMAC: " + (Convert.ToBase64String(sha256hmacForStr)));
        break;

    case '3':
        var sha512hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha512", key);
        Console.WriteLine("\nHash SHA512 HMAC: " + (Convert.ToBase64String(sha512hmacForStr)));
        break;
}

class hash
{
    public static byte[] ComputeHash(byte[] toBeHashed, string metod, byte[] key)
    {
        switch (metod)
        {
            case "hmacmd5":
                using (var hmacmd5 = new HMACMD5(key))
                {
                    return hmacmd5.ComputeHash(toBeHashed);
                }
                break;

            case "hmacsha256":
                using (var hmacsha256 = new HMACSHA256(key))
                {
                    return hmacsha256.ComputeHash(toBeHashed);
                }
                break;

            case "hmacsha512":
                using (var hmacsha512 = new HMACSHA512(key))
                {
                    return hmacsha512.ComputeHash(toBeHashed);
                }
                break;
        }
        return toBeHashed;
    }
}