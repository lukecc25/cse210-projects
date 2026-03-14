using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public SimpleGoal(string name, string description, int points, bool isComplete)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
            return 0;
        _isComplete = true;
        return GetPoints();
    }

    public override string GetDisplayString()
    {
        string mark = _isComplete ? "[X]" : "[ ]";
        return $"{mark} {GetName()}";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{GetName()}|{GetDescription()}|{GetPoints()}|{_isComplete}";
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }
}
