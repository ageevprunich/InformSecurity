using System;
using System.Text;

string path = "C:\\Users\\ageev\\Desktop\\IS\\LW2\\text.txt";
string pathencription = "C:\\Users\\ageev\\Desktop\\IS\\LW2\\encfile5.dat";
string text;

Console.WriteLine("Change message? y - yes , n - no");
char choose = Convert.ToChar(Console.ReadLine());

switch(choose)
{
    case 'y':
        Console.WriteLine("Enter new message");
        text = Console.ReadLine();
        using (StreamWriter write = new StreamWriter(path))
            write.WriteLine(text);
        break;

    case 'n':
    default:
        using (StreamReader read = File.OpenText(path))
            text = read.ReadToEnd();
        break;

}
Console.WriteLine("\nMessage in file: " + text);

string key;
do
{
    Console.Write("Enter password: ");
    key = Console.ReadLine();
    Console.Write("Please,don't forgot this password");
} while (key.Length == 0);

byte[] btext = Encoding.UTF8.GetBytes(text);
byte[] bkey = Encoding.UTF8.GetBytes(key);
byte[] bencmsg = new byte[btext.Length];

for (int i = 0, j = 0; i < btext.Length; i++)
{
    if (j == bkey.Length)
        j = 0;
    byte bencmsgel = (byte)(btext[i] ^ bkey[j]);
    bencmsg[i] = bencmsgel;
    j++;
}

using (FileStream file = new FileStream(pathencription, FileMode.OpenOrCreate))
    file.Write(bencmsg);

Console.WriteLine("\nAll done! Message was encrypted and saved!");
