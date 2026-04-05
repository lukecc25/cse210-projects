using System;

public class GenericPlaceholder : Placeholder
{
    private string _slotLabel;

    public GenericPlaceholder(string slotLabel) : base("{" + (slotLabel ?? "word") + "}")
    {
        _slotLabel = string.IsNullOrWhiteSpace(slotLabel) ? "word" : slotLabel.Trim();
    }

    public override string GetPrompt()
    {
        return $"Enter a word for {{{_slotLabel}}}:";
    }

    public override string GetExample()
    {
        return $"For {{{_slotLabel}}}, use any short word that fits the blank in your story (for example a name, place, or thing that matches the context).";
    }
}
