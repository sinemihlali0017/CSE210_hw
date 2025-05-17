using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Date;
    public string Prompt;
    public string Response;
}

class Program
{
    static List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    static List<Entry> journal = new List<Entry>();

    static void Main(string[] args)
    {
        bool running = true;

        Console.WriteLine("Welcome to the Journal Program!");

        while (running)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option (1-5): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Please enter a number between 1 and 5.");
                    break;
            }
        }
    }

    static void WriteNewEntry()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine("Prompt: " + prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry entry = new Entry
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            Prompt = prompt,
            Response = response
        };

        journal.Add(entry);
        Console.WriteLine("Entry saved!");
    }

    static void DisplayJournal()
    {
        if (journal.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        Console.WriteLine("Your Journal Entries:");
        foreach (Entry entry in journal)
        {
            Console.WriteLine("--------------------------");
            Console.WriteLine("Date: " + entry.Date);
            Console.WriteLine("Prompt: " + entry.Prompt);
            Console.WriteLine("Response: " + entry.Response);
        }
    }

    static void SaveJournalToFile()
    {
        Console.Write("Enter filename to save to (example: journal.txt): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in journal)
                {
                    writer.WriteLine(entry.Date);
                    writer.WriteLine(entry.Prompt);
                    writer.WriteLine(entry.Response);
                    writer.WriteLine(); 
                }
            }

            Console.WriteLine("Journal saved successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error saving the file: " + e.Message);
        }
    }

    static void LoadJournalFromFile()
    {
        Console.Write("Enter filename to load (example: journal.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("That file does not exist.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);
            journal.Clear();

            for (int i = 0; i < lines.Length;)
            {
                if (i + 2 < lines.Length)
                {
                    Entry entry = new Entry
                    {
                        Date = lines[i],
                        Prompt = lines[i + 1],
                        Response = lines[i + 2]
                    };

                    journal.Add(entry);
                    i += 4; // Skip the blank line
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error loading the file: " + e.Message);
        }
    }
}
