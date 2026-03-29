using System;

public class MadLibTemplate
{
    private string _title;
    private string _rawText;

    public MadLibTemplate(string title, string rawText)
    {
        _title = title ?? "";
        _rawText = rawText ?? "";
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetRawText()
    {
        return _rawText;
    }

    public void SetRawText(string text)
    {
        _rawText = text ?? "";
    }

    public string GetStringRepresentation()
    {
        return _title + Environment.NewLine + "---" + Environment.NewLine + _rawText;
    }
}
