using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Journal
{
    // Chosen separator (core requirement allows a "unlikely to appear" separator).
    // Exceeding requirement: JSON save/load also supported.
    private const string Separator = "~|~";

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
            if (Contains(entry.Date, keyword) ||
                Contains(entry.Prompt, keyword) ||
                Contains(entry.Response, keyword) ||
                Contains(entry.Tags, keyword))
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
            if (IsJson(filename))
            {
                SaveToJson(filename);
            }
            else
            {
                SaveToText(filename);
            }

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
            if (IsJson(filename))
            {
                LoadFromJson(filename);
            }
            else
            {
                LoadFromText(filename);
            }

            Console.WriteLine("Journal loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    private static bool IsJson(string filename)
    {
        return filename.Trim().EndsWith(".json", StringComparison.OrdinalIgnoreCase);
    }

    private void SaveToText(string filename)
    {
        using StreamWriter outputFile = new StreamWriter(filename);
        foreach (Entry entry in _entries)
        {
            // Date ~|~ Prompt ~|~ Response ~|~ Mood ~|~ Tags
            outputFile.WriteLine(
                $"{Sanitize(entry.Date)}{Separator}" +
                $"{Sanitize(entry.Prompt)}{Separator}" +
                $"{Sanitize(entry.Response)}{Separator}" +
                $"{entry.Mood}{Separator}" +
                $"{Sanitize(entry.Tags)}"
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

            string[] parts = line.Split(Separator);

            // Support both the core 3-field format and the extended 5-field format.
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

    private void SaveToJson(string filename)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(filename, json);
    }

    private void LoadFromJson(string filename)
    {
        string json = File.ReadAllText(filename);
        List<Entry> loaded = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
        _entries = loaded;
    }

    private static string Sanitize(string value)
    {
        // Keep it simple: strip newlines so one entry = one line.
        return (value ?? "").Replace("\r", " ").Replace("\n", " ");
    }

    private static bool Contains(string haystack, string needle)
    {
        return (haystack ?? "").IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}

