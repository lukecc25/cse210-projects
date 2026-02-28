using System;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    protected int GetDuration()
    {
        return _duration;
    }

    public string GetName()
    {
        return _name;
    }

    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
    }

    public int GetDurationFromUser()
    {
        Console.Write("How long, in seconds, would you like for your session? ");
        string input = Console.ReadLine() ?? "0";
        while (!int.TryParse(input, out _duration) || _duration <= 0)
        {
            Console.Write("Please enter a positive number of seconds: ");
            input = Console.ReadLine() ?? "0";
        }
        return _duration;
    }

    public void DisplayPrepareToBegin()
    {
        Console.WriteLine();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_name} in {_duration} seconds.");
        ShowSpinner(3);
    }

    public void ShowSpinner(int seconds)
    {
        char[] spinner = { '|', '/', '-', '\\' };
        int index = 0;
        DateTime end = DateTime.Now.AddSeconds(seconds);
        while (DateTime.Now < end)
        {
            Console.Write($"\r{spinner[index]} ");
            index = (index + 1) % spinner.Length;
            Thread.Sleep(200);
        }
        Console.Write("\r   \r");
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.Write($"\r{i} ");
            Thread.Sleep(1000);
        }
        Console.Write("\r   \r");
    }

    public abstract void Run();
}
