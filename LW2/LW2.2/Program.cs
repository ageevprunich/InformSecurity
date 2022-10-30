using System;
using System.Text;

string pathencription = "C:\\Users\\ageev\\Desktop\\IS\\LW2\\encfile5.dat";
string text;

using (StreamReader read = File.OpenText(pathencription))
    text = read.ReadToEnd();

Console.WriteLine("\nEncrypted message: " + text);
Console.Write("\nEnter password: ");
string key = Console.ReadLine();

byte[] btext = new byte[text.Length];
btext = Encoding.UTF8.GetBytes(text);
byte[] bkey = Encoding.UTF8.GetBytes(key);
byte[] bdecmsg = new byte[btext.Length];

for (int i = 0, j = 0; i < btext.Length; i++)
{
    if (j == bkey.Length)
        j = 0;
    byte bdecmsgel = (byte)(btext[i] ^ bkey[j]);
    bdecmsg[i] = bdecmsgel;
    j++;
}
Console.WriteLine("\nAll done!");
Console.WriteLine("\nDecrypted message: " + Encoding.UTF8.GetString(bdecmsg));
