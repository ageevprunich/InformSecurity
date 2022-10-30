using System;
using System.Text;
using System.Security.Cryptography;
//MD5
static byte[] ComputeHashMd5(byte[] dataForHash)
{
    using (var md5 = MD5.Create())
    {
        return md5.ComputeHash(dataForHash);
    }
}
//SHA1
static byte[] ComputeHashSha1(byte[] toBeHashed)
{
    using (var sha1 = SHA1.Create())
    {
        return sha1.ComputeHash(toBeHashed);
    }
}
//SHA256
static byte[] ComputeHashSha256(byte[] toBeHashed)
{
    using (var sha256 = SHA256.Create())
    {
        return sha256.ComputeHash(toBeHashed);
    }
}

//SHA512
static byte[] ComputeHashSha512(byte[] toBeHashed)
{
    using (var sha512 = SHA512.Create())
    {
        return sha512.ComputeHash(toBeHashed);
    }
}

//Write
const string strForHash1 = "Hello World!";
var md5ForStr1 = ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash1));
Guid guid1 = new Guid(md5ForStr1);
Console.WriteLine($"Str:{strForHash1}");
Console.WriteLine($"Hash MD5:{Convert.ToBase64String(md5ForStr1)}");
Console.WriteLine($"GUID:{guid1}");
Console.WriteLine("-------------------------------------------------------");
var sha1ForStr1 = ComputeHashSha1(Encoding.Unicode.GetBytes(strForHash1));
Console.WriteLine($"Hash SHA1:{Convert.ToBase64String(sha1ForStr1)}");
Console.WriteLine("-------------------------------------------------------");
var sha256ForStr1 = ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash1));
Console.WriteLine($"Hash SHA256:{Convert.ToBase64String(sha256ForStr1)}");
Console.WriteLine("-------------------------------------------------------");
var sha512ForStr1 = ComputeHashSha512(Encoding.Unicode.GetBytes(strForHash1));
Console.WriteLine($"Hash SHA512:{Convert.ToBase64String(sha512ForStr1)}");

