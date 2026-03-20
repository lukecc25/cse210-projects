using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private const string _separator = "~|~";

    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        if (entry != null)
        {
            _entries.Add(entry);
        }
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Journal is empty.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SearchAndDisplay(string keyword)
    {
        keyword = keyword ?? "";

        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.WriteLine("Please enter a keyword.");
            return;
        }

        int matches = 0;
        foreach (Entry entry in _entries)
        {
            if (Contains(entry.GetDate(), keyword) ||
                Contains(entry.GetPrompt(), keyword) ||
                Contains(entry.GetResponse(), keyword) ||
                Contains(entry.GetTags(), keyword))
            {
                entry.Display();
                matches++;
            }
        }

        if (matches == 0)
        {
            Console.WriteLine("No matches found.");
        }
        else
        {
            Console.WriteLine($"Matches: {matches}");
        }
    }

    public void SaveToFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.");
            return;
        }

        try
        {
            SaveToText(filename);

            Console.WriteLine("Journal saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.");
            return;
        }

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            LoadFromText(filename);

            Console.WriteLine("Journal loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    private void SaveToText(string filename)
    {
        using StreamWriter outputFile = new StreamWriter(filename);
        foreach (Entry entry in _entries)
        {
            outputFile.WriteLine(
                $"{Sanitize(entry.GetDate())}{_separator}" +
                $"{Sanitize(entry.GetPrompt())}{_separator}" +
                $"{Sanitize(entry.GetResponse())}{_separator}" +
                $"{entry.GetMood()}{_separator}" +
                $"{Sanitize(entry.GetTags())}"
            );
        }
    }

    private void LoadFromText(string filename)
    {
        List<Entry> loaded = new List<Entry>();

        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split(_separator);

            if (parts.Length >= 3)
            {
                string date = parts[0];
                string prompt = parts[1];
                string response = parts[2];

                int mood = 0;
                string tags = "";

                if (parts.Length >= 4)
                {
                    int.TryParse(parts[3], out mood);
                }

                if (parts.Length >= 5)
                {
                    tags = parts[4];
                }

                loaded.Add(new Entry(date, prompt, response, mood, tags));
            }
        }

        _entries = loaded;
    }

    private static string Sanitize(string value)
    {
        return (value ?? "").Replace("\r", " ").Replace("\n", " ");
    }

    private static bool Contains(string haystack, string needle)
    {
        return (haystack ?? "").IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}

