using System;
using System.Collections.Generic;

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
            string exactMarker = rawText.Substring(start, end - start + 1);
            string inner = rawText.Substring(start + 1, end - start - 1).Trim();
            if (inner.Length > 0)
                slots.Add(new PlaceholderSlot(inner, order++, exactMarker));
            i = end + 1;
        }
        return slots;
    }

    public string BuildFinalStory(string rawText, List<PlaceholderSlot> slots)
    {
        if (string.IsNullOrEmpty(rawText))
            return "";
        if (slots == null || slots.Count == 0)
            return rawText;

        string result = rawText;
        foreach (PlaceholderSlot slot in slots)
        {
            string marker = slot.GetExactMarker();
            string replacement = slot.GetUserWord() ?? "";
            if (string.IsNullOrEmpty(marker))
                continue;
            int idx = result.IndexOf(marker, StringComparison.Ordinal);
            if (idx >= 0)
                result = result.Substring(0, idx) + replacement + result.Substring(idx + marker.Length);
        }
        return result;
    }
}
