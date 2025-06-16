using System;
using System.Collections.Generic;
using System.IO;


public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStatus();
    public abstract string GetSaveData();
}

// Simple Goal
public class SimpleGoal : Goal
{
    private bool _isDone;

    public SimpleGoal(string name, string description, int points, bool isDone = false)
        : base(name, description, points)
    {
        _isDone = isDone;
    }

    public override int RecordEvent()
    {
        if (!_isDone)
        {
            _isDone = true;
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => _isDone;

    public override string GetStatus()
    {
        return (_isDone ? "[X] " : "[ ] ") + Name + " - " + Description;
    }

    public override string GetSaveData()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{_isDone}";
    }
}


public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => Points;

    public override bool IsComplete() => false;

    public override string GetStatus()
    {
        return "[∞] " + Name + " - " + Description;
    }

    public override string GetSaveData()
    {
        return $"EternalGoal|{Name}|{Description}|{Points}";
    }
}


public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _timesRequired;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int required, int bonus, int completed = 0)
        : base(name, description, points)
    {
        _timesRequired = required;
        _bonus = bonus;
        _timesCompleted = completed;
    }

    public override int RecordEvent()
    {
        if (_timesCompleted < _timesRequired)
        {
            _timesCompleted++;
            if (_timesCompleted == _timesRequired)
                return Points + _bonus;
            return Points;
        }
        return 0;
    }

    public override bool IsComplete() => _timesCompleted >= _timesRequired;

    public override string GetStatus()
    {
        return "[" + (IsComplete() ? "X" : " ") + $"] {Name} - {Description} ({_timesCompleted}/{_timesRequired})";
    }

    public override string GetSaveData()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{_timesRequired}|{_bonus}|{_timesCompleted}";
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalPoints = 0;

    static void Main(string[] args)
    {
        string choice = "";

        while (choice != "7")
        {
            Console.WriteLine("\n--- Eternal Quest ---");
            Console.WriteLine($"Total Points: {totalPoints} | Level: {totalPoints / 1000 + 1}");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Help");
            Console.WriteLine("7. Quit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordGoal(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": ShowHelp(); break;
                case "7": Console.WriteLine("Goodbye Adventurer!"); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("\nSelect Goal Type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
        {
            goals.Add(new SimpleGoal(name, desc, points));
        }
        else if (type == "2")
        {
            goals.Add(new EternalGoal(name, desc, points));
        }
        else if (type == "3")
        {
            Console.Write("Times to complete: ");
            int times = int.Parse(Console.ReadLine());
            Console.Write("Bonus points: ");
            int bonus = int.Parse(Console.ReadLine());
            goals.Add(new ChecklistGoal(name, desc, points, times, bonus));
        }
        else
        {
            Console.WriteLine("Invalid type.");
        }
    }

    static void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
    }

    static void RecordGoal()
    {
        ListGoals();
        Console.Write("Which goal did you complete? ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < goals.Count)
        {
            int earned = goals[index].RecordEvent();
            totalPoints += earned;
            Console.WriteLine($"You earned {earned} points!");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(totalPoints);
            foreach (Goal g in goals)
            {
                writer.WriteLine(g.GetSaveData());
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    static void LoadGoals()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No saved data found.");
            return;
        }

        goals.Clear();
        string[] lines = File.ReadAllLines("goals.txt");
        totalPoints = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0];
            string name = parts[1];
            string desc = parts[2];
            int pts = int.Parse(parts[3]);

            if (type == "SimpleGoal")
            {
                bool isDone = bool.Parse(parts[4]);
                goals.Add(new SimpleGoal(name, desc, pts, isDone));
            }
            else if (type == "EternalGoal")
            {
                goals.Add(new EternalGoal(name, desc, pts));
            }
            else if (type == "ChecklistGoal")
            {
                int req = int.Parse(parts[4]);
                int bonus = int.Parse(parts[5]);
                int comp = int.Parse(parts[6]);
                goals.Add(new ChecklistGoal(name, desc, pts, req, bonus, comp));
            }
        }

        Console.WriteLine("Goals loaded.");
    }

    static void ShowHelp()
    {
        Console.WriteLine("\nHelp:");
        Console.WriteLine("- Simple Goal: Done once, gives full points.");
        Console.WriteLine("- Eternal Goal: Never done, gives points each time.");
        Console.WriteLine("- Checklist Goal: Do multiple times to get bonus.");
        Console.WriteLine("- Level Up: You level up every 1000 points!");
    }
}
