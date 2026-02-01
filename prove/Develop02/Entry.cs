using System;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    
    public int Mood { get; set; } // 0 means "not provided"
    public string Tags { get; set; }

    public Entry()
    {
        Date = "";
        Prompt = "";
        Response = "";
        Mood = 0;
        Tags = "";
    }

    public Entry(string date, string prompt, string response, int mood = 0, string tags = "")
    {
        Date = date ?? "";
        Prompt = prompt ?? "";
        Response = response ?? "";
        Mood = mood;
        Tags = tags ?? "";
    }

    public void Display()
    {
        Console.WriteLine($"Date: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");

        if (Mood != 0)
        {
            Console.WriteLine($"Mood: {Mood}/5");
        }

        if (!string.IsNullOrWhiteSpace(Tags))
        {
            Console.WriteLine($"Tags: {Tags}");
        }

        Console.WriteLine();
    }
}

