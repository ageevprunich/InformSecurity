var rnd = new Random(99);

for (int i = 5; i < 10; i++)
{
    var nrnd = rnd.Next(0, 10);
    Console.WriteLine(nrnd);
}