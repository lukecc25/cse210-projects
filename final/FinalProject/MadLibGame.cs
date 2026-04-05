using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MadLibGame
{
    private MadLibParser _parser;
    private MadLibRepository _repository;
    private MadLibTemplate _lastTemplate;

    public MadLibGame(MadLibParser parser, MadLibRepository repository)
    {
        _parser = parser;
        _repository = repository;
    }

    public MadLibTemplate CreateTemplateFromUser()
    {
        Console.Write("Title for this Mad Lib: ");
        string title = Console.ReadLine() ?? "";
        Console.WriteLine("Enter the story text. Use {NOUN}, {VERB}, {ADJ} or {ADJECTIVE} as placeholders.");
        Console.WriteLine("Press Enter on an empty line when you are done.");
        StringBuilder body = new StringBuilder();
        while (true)
        {
            string line = Console.ReadLine();
            if (line == null || line.Length == 0)
                break;
            if (body.Length > 0)
                body.AppendLine();
            body.Append(line);
        }
        return new MadLibTemplate(title, body.ToString());
    }

    public void PlayTemplate(MadLibTemplate template)
    {
        if (template == null)
        {
            Console.WriteLine("No template to play.");
            return;
        }

        _lastTemplate = template;
        List<PlaceholderSlot> slots = _parser.ParseSlots(template.GetRawText());
        if (slots.Count == 0)
        {
            Console.WriteLine("This story has no {placeholder} markers. Here it is unchanged:");
            Console.WriteLine();
            Console.WriteLine(template.GetRawText());
            return;
        }

        foreach (PlaceholderSlot slot in slots)
        {
            Placeholder guide = CreatePlaceholderGuide(slot.GetPlaceholderType());
            Console.WriteLine(guide.GetPrompt() + " (type example or ? for help)");
            PromptUntilWordEntered(slot, guide);
        }

        string story = _parser.BuildFinalStory(template.GetRawText(), slots);
        Console.WriteLine();
        Console.WriteLine("--- Your Mad Lib ---");
        Console.WriteLine(story);
    }

    private static Placeholder CreatePlaceholderGuide(string type)
    {
        string upper = (type ?? "").ToUpperInvariant();
        if (upper == "NOUN")
            return new NounPlaceholder();
        if (upper == "VERB")
            return new VerbPlaceholder();
        if (upper == "ADJ")
            return new AdjectivePlaceholder();
        return new GenericPlaceholder(type ?? "word");
    }

    private static void PromptUntilWordEntered(PlaceholderSlot slot, Placeholder guide)
    {
        while (true)
        {
            Console.Write("> ");
            string line = Console.ReadLine()?.Trim() ?? "";
            if (line.Equals("example", StringComparison.OrdinalIgnoreCase) || line == "?")
            {
                Console.WriteLine(guide.GetExample());
                continue;
            }
            slot.SetUserWord(line);
            break;
        }
    }

    private void LoadAndPlay()
    {
        List<string> files = _repository.ListTemplateFiles();
        if (files.Count == 0)
        {
            Console.WriteLine("No saved .txt templates yet. Use option 4 after saving one.");
            return;
        }
        Console.WriteLine("Saved templates:");
        for (int i = 0; i < files.Count; i++)
            Console.WriteLine($"  {i + 1}. {files[i]}");
        Console.Write("Enter number or filename (e.g. mylib): ");
        string input = Console.ReadLine()?.Trim() ?? "";
        if (input.Length == 0)
            return;

        string filename = input;
        if (int.TryParse(input, out int n) && n >= 1 && n <= files.Count)
            filename = files[n - 1];

        MadLibTemplate loaded = _repository.LoadTemplate(filename);
        if (loaded == null)
        {
            Console.WriteLine("Could not load that template.");
            return;
        }
        Console.WriteLine($"Loaded: {loaded.GetTitle()}");
        PlayTemplate(loaded);
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Mad Libs");
            Console.WriteLine("While filling blanks, type example or ? for a part-of-speech example.");
            Console.WriteLine("1. Play demo story");
            Console.WriteLine("2. Create a template (then play it)");
            Console.WriteLine("3. Save last played template");
            Console.WriteLine("4. List saved templates");
            Console.WriteLine("5. Load a template and play");
            Console.WriteLine("6. Quit");
            Console.Write("> ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "6")
                break;

            if (choice == "1")
            {
                MadLibTemplate demo = new MadLibTemplate(
                    "Demo",
                    "The {ADJ} {NOUN} likes to {VERB} every day.");
                PlayTemplate(demo);
            }
            else if (choice == "2")
            {
                MadLibTemplate t = CreateTemplateFromUser();
                PlayTemplate(t);
            }
            else if (choice == "3")
            {
                if (_lastTemplate == null)
                {
                    Console.WriteLine("Play or create a template first (options 1, 2, or 5).");
                    continue;
                }
                Console.Write("Filename (e.g. mylib or mylib.txt): ");
                string fn = Console.ReadLine()?.Trim() ?? "saved.txt";
                _repository.SaveTemplate(_lastTemplate, fn);
                string leaf = fn;
                if (!leaf.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    leaf += ".txt";
                Console.WriteLine($"Saved to: {Path.Combine(_repository.GetBaseDirectory(), Path.GetFileName(leaf))}");
            }
            else if (choice == "4")
            {
                List<string> names = _repository.ListTemplateFiles();
                if (names.Count == 0)
                    Console.WriteLine("(No .txt files yet.)");
                else
                {
                    foreach (string name in names)
                        Console.WriteLine("  " + name);
                }
            }
            else if (choice == "5")
            {
                LoadAndPlay();
            }
            else
            {
                Console.WriteLine("Unknown option.");
            }
        }
    }
}
