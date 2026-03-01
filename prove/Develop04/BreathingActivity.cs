using System;

public class BreathingActivity : Activity
{
    private const int SecondsPerBreath = 4;

    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing."
        )
    {
    }

    public override void Run()
    {
        int durationSeconds = GetDuration();
        DisplayPrepareToBegin();
        Console.WriteLine();

        int elapsed = 0;
        bool breatheIn = true;
        while (elapsed < durationSeconds)
        {
            int segment = Math.Min(SecondsPerBreath, durationSeconds - elapsed);
            if (breatheIn)
            {
                Console.WriteLine("Breathe in...");
            }
            else
            {
                Console.WriteLine("Breathe out...");
            }
            ShowCountDown(segment);
            elapsed += segment;
            breatheIn = !breatheIn;
        }

        DisplayEndingMessage();
    }
}
