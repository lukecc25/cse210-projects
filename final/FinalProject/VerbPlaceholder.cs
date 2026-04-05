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

    public override string GetExample()
    {
        return "A verb is an action or state. Examples: sprint, wonder, melt.";
    }
}
