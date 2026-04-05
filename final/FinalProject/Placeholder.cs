using System;

public abstract class Placeholder
{
    private string _marker;

    public Placeholder(string marker)
    {
        _marker = marker;
    }

    public string GetMarker()
    {
        return _marker;
    }

    public abstract string GetPrompt();

    public abstract string GetExample();
}
