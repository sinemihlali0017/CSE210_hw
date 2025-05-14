using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World! This is the Exercise5 Project.");

        DisplayMessage();

        string name = GetUserName();
        Console.WriteLine("Nice to meet you, " + name + "!");

        int number = PromptUserNumber();
        Console.WriteLine("You entered the number: " + number);

        int squareNumber = PromptSquareNumber();
        Console.WriteLine("The square root of your number is: " + squareNumber);
    }

    static void DisplayMessage()
    {
        Console.WriteLine("Welcome to the program");
    }

    static string GetUserName()
    {
        Console.Write("Please enter your name: ");
        string userName = Console.ReadLine();
        return userName;
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your number: ");
        string userInput = Console.ReadLine();
        int number = int.Parse(userInput);
        return number;
    }

    static int PromptSquareNumber()
    {
        Console.Write("Please enter a number to find its square root: ");
        string userInput = Console.ReadLine();
        int number = int.Parse(userInput);


        double squareRoot = Math.Sqrt(number);

        int roundedSquareRoot = (int)Math.Round(squareRoot);
        return roundedSquareRoot;
    }
}

