using System;

// Exceeding requirements:
// - Adds optional "Mood (1-5)" and "Tags" to each entry (stored and displayed).
// - Save/Load supports both the core custom text format AND JSON when the filename ends in ".json".
// - Adds a "Search entries" menu option to find entries by keyword.
class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator prompts = new PromptGenerator();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.WriteLine("6. Search");
            Console.Write("> ");

            string choice = (Console.ReadLine() ?? "").Trim();

            if (choice == "1")
            {
                string prompt = prompts.GetRandomPrompt();
                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine() ?? "";

                int mood = ReadMood();

                Console.Write("Tags (optional, comma-separated): ");
                string tags = Console.ReadLine() ?? "";

                Entry entry = new Entry(
                    date: DateTime.Now.ToString("yyyy-MM-dd"),
                    prompt: prompt,
                    response: response,
                    mood: mood,
                    tags: tags
                );

                journal.AddEntry(entry);
            }
            else if (choice == "2")
            {
                journal.DisplayAll();
            }
            else if (choice == "3")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine() ?? "";
                journal.LoadFromFile(filename);
            }
            else if (choice == "4")
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine() ?? "";
                journal.SaveToFile(filename);
            }
            else if (choice == "5")
            {
                break;
            }
            else if (choice == "6")
            {
                Console.Write("Search keyword: ");
                string keyword = Console.ReadLine() ?? "";
                journal.SearchAndDisplay(keyword);
            }
            else
            {
                Console.WriteLine("Please enter a valid option (1-6).");
            }
        }
    }

    private static int ReadMood()
    {
        while (true)
        {
            Console.Write("Mood (1-5, optional - press Enter to skip): ");
            string input = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            if (int.TryParse(input, out int mood) && mood >= 1 && mood <= 5)
            {
                return mood;
            }

            Console.WriteLine("Please enter a number from 1 to 5 (or press Enter to skip).");
        }
    }
}
