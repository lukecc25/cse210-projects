using System;

public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _targetCount = target;
        _currentCount = 0;
        _bonusPoints = bonus;
    }

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int currentCount)
        : base(name, description, points)
    {
        _targetCount = target;
        _currentCount = currentCount;
        _bonusPoints = bonus;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        int earned = GetPoints();
        if (_currentCount >= _targetCount)
            earned += _bonusPoints;
        return earned;
    }

    public override string GetDisplayString()
    {
        string mark = _currentCount >= _targetCount ? "[X]" : "[ ]";
        return $"{mark} {GetName()} -- Completed {_currentCount}/{_targetCount} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{GetName()}|{GetDescription()}|{GetPoints()}|{_targetCount}|{_bonusPoints}|{_currentCount}";
    }

    public override bool IsComplete()
    {
        return _currentCount >= _targetCount;
    }
}
