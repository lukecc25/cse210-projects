using System;
using System.Collections.Generic;

// Exceeding requirements: The program uses a library of scriptures and selects one at random
// for the user to memorize. The user can type 'new' at any time for a different scripture
// without quitting the program.
class Program
{
    static void Main(string[] args)
    {
        List<Scripture> library = GetScriptureLibrary();
        Random random = new Random();
        Scripture scripture = library[random.Next(library.Count)];

        const int wordsToHidePerRound = 3;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide more words, type 'new' for a different scripture, or type 'quit' to end.");
            string input = Console.ReadLine() ?? "";
            string choice = input.Trim().ToLower();

            if (choice == "quit")
            {
                break;
            }

            if (choice == "new")
            {
                scripture = library[random.Next(library.Count)];
                continue;
            }

            scripture.HideRandomWords(wordsToHidePerRound);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine();
                Console.WriteLine("All words are hidden. Well done!");
                break;
            }
        }
    }

    private static List<Scripture> GetScriptureLibrary()
    {
        return new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."
            ),
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."
            ),
            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me."
            ),
            new Scripture(
                new Reference("Psalm", 23, 1),
                "The Lord is my shepherd; I shall not want."
            ),
            new Scripture(
                new Reference("Matthew", 11, 28),
                "Come unto me, all ye that labour and are heavy laden, and I will give you rest."
            )
        };
    }
}
