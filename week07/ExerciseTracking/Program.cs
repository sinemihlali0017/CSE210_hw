using System;
using System.Collections.Generic;


public abstract class Activity
{
    private string _date;
    private int _minutes;

    public Activity(string date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public string GetDate()
    {
        return _date;
    }

    public int GetMinutes()
    {
        return _minutes;
    }

    public abstract double GetDistance(); // in kilometers
    public abstract double GetSpeed();    // in kph
    public abstract double GetPace();     // in min per km

    public virtual string GetSummary()
    {
        return $"{_date} {this.GetType().Name} ({_minutes} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.00} min per km";
    }
}


public class Running : Activity
{
    private double _distance; // in km

    public Running(string date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / _distance;
    }
}


public class Cycling : Activity
{
    private double _speed; // in kph

    public Cycling(string date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetDistance()
    {
        return (_speed * GetMinutes()) / 60;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}

public class Swimming : Activity
{
    private int _laps;

    public Swimming(string date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return (_laps * 50) / 1000.0; // 50 meters per lap
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / GetDistance();
    }
}


class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

    
        Running run = new Running("03 Nov 2022", 30, 4.8); // km
        Cycling bike = new Cycling("04 Nov 2022", 45, 20);  // kph
        Swimming swim = new Swimming("05 Nov 2022", 30, 40); // laps

    
        activities.Add(run);
        activities.Add(bike);
        activities.Add(swim);

        
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

