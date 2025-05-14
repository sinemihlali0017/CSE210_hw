
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {

        List<int> numbers = new List<int>();
        int userInput;

        Console.WriteLine("Enter a series of numbers. Enter 0 to stop:");


        do
        {
            Console.Write("Enter a number: ");
            userInput = int.Parse(Console.ReadLine());

            if (userInput != 0)
            {
                numbers.Add(userInput);
            }

        } while (userInput != 0);


        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered.");
        }
        else
        {

            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }


            double average = (double)sum / numbers.Count;


            int max = numbers[0];
            foreach (int number in numbers)
            {
                if (number > max)
                {
                    max = number;
                }
            }


            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Average: {average}");
            Console.WriteLine($"Maximum: {max}");
        }
    }
}
