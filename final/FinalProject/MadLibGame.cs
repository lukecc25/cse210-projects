using System;
using System.Collections.Generic;

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
        Console.WriteLine("Enter the story text. Use markers like {NOUN}, {VERB}, {ADJ} on one line for now:");
        string raw = Console.ReadLine() ?? "";
        return new MadLibTemplate(title, raw);
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
        foreach (PlaceholderSlot slot in slots)
        {
            Console.WriteLine(GetPromptForType(slot.GetPlaceholderType()));
            Console.Write("> ");
            slot.SetUserWord(Console.ReadLine() ?? "");
        }

        string story = _parser.BuildFinalStory(template.GetRawText(), slots);
        Console.WriteLine();
        Console.WriteLine("--- Your Mad Lib ---");
        Console.WriteLine(story);
    }

    private static string GetPromptForType(string type)
    {
        string upper = (type ?? "").ToUpperInvariant();
        if (upper == "NOUN")
            return new NounPlaceholder().GetPrompt();
        if (upper == "VERB")
            return new VerbPlaceholder().GetPrompt();
        if (upper == "ADJ")
            return new AdjectivePlaceholder().GetPrompt();
        return $"Enter a word for {{{type}}}:";
    }

    public void RunMenu()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Mad Libs (prototype)");
            Console.WriteLine("1. Play demo story");
            Console.WriteLine("2. Create a template (then play it)");
            Console.WriteLine("3. Save last created template (stub)");
            Console.WriteLine("4. List saved templates");
            Console.WriteLine("5. Quit");
            Console.Write("> ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "5")
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
                    Console.WriteLine("Play or create a template first (options 1 or 2).");
                    continue;
                }
                Console.Write("Filename (e.g. mylib.txt): ");
                string fn = Console.ReadLine()?.Trim() ?? "saved.txt";
                _repository.SaveTemplate(_lastTemplate, fn);
                Console.WriteLine("Template saved.");
            }
            else if (choice == "4")
            {
                foreach (string name in _repository.ListTemplateFiles())
                    Console.WriteLine("  " + name);
                if (_repository.ListTemplateFiles().Count == 0)
                    Console.WriteLine("(No .txt files yet.)");
            }
            else
            {
                Console.WriteLine("Unknown option.");
            }
        }
    }
}
