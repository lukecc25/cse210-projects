using System;
using System.Collections.Generic;
using System.Text;

public class MadLibParser
{
    public MadLibParser()
    {
    }

    public List<PlaceholderSlot> ParseSlots(string rawText)
    {
        List<PlaceholderSlot> slots = new List<PlaceholderSlot>();
        if (string.IsNullOrEmpty(rawText))
            return slots;

        int order = 0;
        int i = 0;
        while (i < rawText.Length)
        {
            int start = rawText.IndexOf('{', i);
            if (start < 0)
                break;
            int end = rawText.IndexOf('}', start);
            if (end < 0)
                break;
            string inner = rawText.Substring(start + 1, end - start - 1).Trim();
            slots.Add(new PlaceholderSlot(inner, order++));
            i = end + 1;
        }
        return slots;
    }

    public string BuildFinalStory(string rawText, List<PlaceholderSlot> slots)
    {
        if (slots == null || slots.Count == 0)
            return rawText ?? "";

        StringBuilder result = new StringBuilder(rawText ?? "");
        foreach (PlaceholderSlot slot in slots)
        {
            string marker = "{" + slot.GetPlaceholderType() + "}";
            int idx = result.ToString().IndexOf(marker, StringComparison.Ordinal);
            if (idx >= 0)
            {
                result.Remove(idx, marker.Length);
                result.Insert(idx, slot.GetUserWord());
            }
        }
        return result.ToString();
    }
}
