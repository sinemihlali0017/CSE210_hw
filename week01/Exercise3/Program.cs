using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise3 Project.");
        Random random = new Random();
        int magicNumber = random.Next(1, 101);




        int guess = 0;

        while (guess != magicNumber)
        {
            Console.WriteLine("what is your guess?");
            string lucku_Guess = Console.ReadLine();
            guess = int.Parse(lucku_Guess);

            if (guess > magicNumber)
            {
                Console.Write("lower ");
            }
            else if (guess < magicNumber)
            {
                Console.Write("higher");
            }
            else
            {
                Console.Write("you guessed it ");
            }
        }
    }
}





