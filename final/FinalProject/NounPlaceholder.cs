using System;

public class NounPlaceholder : Placeholder
{
    public NounPlaceholder() : base("{NOUN}")
    {
    }

    public override string GetPrompt()
    {
        return "Enter a noun:";
    }
}
