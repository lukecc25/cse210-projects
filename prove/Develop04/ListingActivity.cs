using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private Random _random = new Random();

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
        )
    {
    }

    public override void Run()
    {
        int durationSeconds = GetDuration();
        DisplayPrepareToBegin();
        Console.WriteLine();

        string prompt = _prompts[_random.Next(_prompts.Count)];
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {prompt} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountDown(5);
        Console.WriteLine();

        List<string> items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string line = Console.ReadLine()?.Trim() ?? "";
            if (DateTime.Now >= endTime)
                break;
            if (!string.IsNullOrEmpty(line))
                items.Add(line);
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {items.Count} items.");
        Console.WriteLine();
        DisplayEndingMessage();
    }
}
