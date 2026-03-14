using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return GetPoints();
    }

    public override string GetDisplayString()
    {
        return $"[ ] {GetName()}";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{GetName()}|{GetDescription()}|{GetPoints()}";
    }

    public override bool IsComplete()
    {
        return false;
    }
}
