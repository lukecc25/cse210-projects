using System;

public class VerbPlaceholder : Placeholder
{
    public VerbPlaceholder() : base("{VERB}")
    {
    }

    public override string GetPrompt()
    {
        return "Enter a verb:";
    }
}
