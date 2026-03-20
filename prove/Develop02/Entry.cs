using System;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;

    // Exceeding requirement: extra info
    private int _mood; // 0 means "not provided"
    private string _tags;

    public Entry()
    {
        _date = "";
        _prompt = "";
        _response = "";
        _mood = 0;
        _tags = "";
    }

    public Entry(string date, string prompt, string response, int mood = 0, string tags = "")
    {
        _date = date ?? "";
        _prompt = prompt ?? "";
        _response = response ?? "";
        _mood = mood;
        _tags = tags ?? "";
    }

    public string GetDate()
    {
        return _date;
    }

    public string GetPrompt()
    {
        return _prompt;
    }

    public string GetResponse()
    {
        return _response;
    }

    public int GetMood()
    {
        return _mood;
    }

    public string GetTags()
    {
        return _tags;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");

        if (_mood != 0)
        {
            Console.WriteLine($"Mood: {_mood}/5");
        }

        if (!string.IsNullOrWhiteSpace(_tags))
        {
            Console.WriteLine($"Tags: {_tags}");
        }

        Console.WriteLine();
    }
}
