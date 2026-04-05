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

    public override string GetExample()
    {
        return "A noun names a person, place, thing, or idea. Examples: dolphin, kitchen, courage.";
    }
}
