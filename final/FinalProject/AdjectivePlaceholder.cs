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

    public override string GetExample()
    {
        return "An adjective describes a noun. Examples: scratchy, brave, miniature.";
    }
}
