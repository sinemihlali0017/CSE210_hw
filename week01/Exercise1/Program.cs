using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("what is your name?");
        string first = Console.ReadLine();

        Console.WriteLine("what is your last_name?");
        string last_name = Console.ReadLine();

        Console.WriteLine($" your name is {first} , {last_name}");
    }
}