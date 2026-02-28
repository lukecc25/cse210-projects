using System;

// Exceeding requirements: The program keeps a simple session log and displays how many
// activities were completed when the user quits (e.g. "You completed 3 activities this session.").
class Program
{
    static void Main(string[] args)
    {
        int activitiesCompleted = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "4" || choice.ToLower() == "quit")
            {
                Console.WriteLine($"You completed {activitiesCompleted} activit{(activitiesCompleted == 1 ? "y" : "ies")} this session.");
                Console.WriteLine("Goodbye.");
                break;
            }

            Activity activity = null;
            if (choice == "1")
            {
                activity = new BreathingActivity();
            }
            else if (choice == "2")
            {
                activity = new ReflectionActivity();
            }
            else if (choice == "3")
            {
                activity = new ListingActivity();
            }
            else
            {
                Console.WriteLine("Invalid option. Press Enter to return to the menu.");
                Console.ReadLine();
                continue;
            }

            activity.DisplayStartingMessage();
            activity.GetDurationFromUser();
            activity.Run();
            activitiesCompleted++;
        }
    }
}
