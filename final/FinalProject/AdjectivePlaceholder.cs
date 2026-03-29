using System;

public class AdjectivePlaceholder : Placeholder
{
    public AdjectivePlaceholder() : base("{ADJ}")
    {
    }

    public override string GetPrompt()
    {
        return "Enter an adjective:";
    }
}
