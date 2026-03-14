using System;
using System.Collections.Generic;
using System.IO;

// Exceeding requirements: The program shows a simple "level" based on total points
// under level it gives the user what their point total is and what they need to level up.
class Program
{
    static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();
        int score = 0;

        while (true)
        {
            Console.WriteLine();
            int level = Math.Max(1, (score / 1000) + 1);
            int pointsForNextLevel = level * 1000;
            Console.WriteLine($"Level {level}");
            Console.WriteLine($"({score}/{pointsForNextLevel} points to next level)");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "6")
            {
                Console.WriteLine("Goodbye!");
                break;
            }

            if (choice == "1")
            {
                Goal goal = CreateGoal();
                if (goal != null)
                    goals.Add(goal);
            }
            else if (choice == "2")
            {
                ListGoals(goals);
            }
            else if (choice == "3")
            {
                SaveGoals(goals, score);
            }
            else if (choice == "4")
            {
                LoadGoals(out goals, out score);
            }
            else if (choice == "5")
            {
                RecordEvent(goals, ref score);
            }
            else
            {
                Console.WriteLine("Invalid option. Press Enter to continue.");
                Console.ReadLine();
            }
        }
    }

    static Goal CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple goal (complete once for points)");
        Console.WriteLine("  2. Eternal goal (every time you do it you get points)");
        Console.WriteLine("  3. Checklist goal (complete a certain number of times for a bonus)");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine()?.Trim() ?? "";

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine()?.Trim() ?? "";
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine()?.Trim() ?? "";
        Console.Write("What is the amount of points associated with this goal? ");
        string pointsStr = Console.ReadLine()?.Trim() ?? "0";
        int points = int.TryParse(pointsStr, out int p) ? p : 0;

        if (type == "1")
        {
            return new SimpleGoal(name, description, points);
        }
        else if (type == "2")
        {
            return new EternalGoal(name, description, points);
        }
        else if (type == "3")
        {
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            string targetStr = Console.ReadLine()?.Trim() ?? "0";
            int target = int.TryParse(targetStr, out int t) ? t : 0;
            Console.Write("What is the bonus for accomplishing it that many times? ");
            string bonusStr = Console.ReadLine()?.Trim() ?? "0";
            int bonus = int.TryParse(bonusStr, out int b) ? b : 0;
            return new ChecklistGoal(name, description, points, target, bonus);
        }

        Console.WriteLine("Unknown goal type. Goal not created.");
        return null;
    }

    static void ListGoals(List<Goal> goals)
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals yet. Create one from the menu.");
            return;
        }
        Console.WriteLine("The goals are:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {goals[i].GetDisplayString()}");
        }
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    static void RecordEvent(List<Goal> goals, ref int score)
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals yet. Create one from the menu.");
            Console.ReadLine();
            return;
        }
        Console.WriteLine("The goals are:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {goals[i].GetName()}");
        }
        Console.Write("Which goal did you accomplish? ");
        string input = Console.ReadLine()?.Trim() ?? "";
        if (!int.TryParse(input, out int index) || index < 1 || index > goals.Count)
        {
            Console.WriteLine("Invalid selection.");
            Console.ReadLine();
            return;
        }
        Goal goal = goals[index - 1];
        int earned = goal.RecordEvent();
        score += earned;
        Console.WriteLine($"Congratulations! You have earned {earned} points!");
        Console.WriteLine($"You now have {score} points.");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    static void SaveGoals(List<Goal> goals, int score)
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("Filename cannot be empty.");
            Console.ReadLine();
            return;
        }
        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(score);
                foreach (Goal goal in goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
            Console.WriteLine("Goals saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    static void LoadGoals(out List<Goal> goals, out int score)
    {
        goals = new List<Goal>();
        score = 0;
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine()?.Trim() ?? "";
        if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
        {
            Console.WriteLine("File not found or filename empty.");
            Console.ReadLine();
            return;
        }
        try
        {
            string[] lines = File.ReadAllLines(filename);
            if (lines.Length > 0 && int.TryParse(lines[0], out score)) { }
            for (int i = 1; i < lines.Length; i++)
            {
                Goal goal = ParseGoal(lines[i]);
                if (goal != null)
                    goals.Add(goal);
            }
            Console.WriteLine("Goals loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    static Goal ParseGoal(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length < 4) return null;
        string type = parts[0];
        string name = parts[1];
        string description = parts[2];
        if (!int.TryParse(parts[3], out int points)) return null;

        if (type == "SimpleGoal" && parts.Length >= 5)
        {
            bool isComplete = parts[4].Equals("True", StringComparison.OrdinalIgnoreCase);
            return new SimpleGoal(name, description, points, isComplete);
        }
        if (type == "EternalGoal")
        {
            return new EternalGoal(name, description, points);
        }
        if (type == "ChecklistGoal" && parts.Length >= 7)
        {
            if (!int.TryParse(parts[4], out int target)) return null;
            if (!int.TryParse(parts[5], out int bonus)) return null;
            if (!int.TryParse(parts[6], out int current)) return null;
            return new ChecklistGoal(name, description, points, target, bonus, current);
        }
        return null;
    }
}
