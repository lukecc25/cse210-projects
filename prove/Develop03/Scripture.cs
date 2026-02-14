using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        string[] parts = (text ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            _words.Add(new Word(part));
        }
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        int hidden = 0;
        int attempts = 0;
        int maxAttempts = _words.Count * 3;
        while (hidden < count && attempts < maxAttempts)
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                hidden++;
            }
            attempts++;
        }
    }

    public bool AllWordsHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }

    public string GetDisplayText()
    {
        string refDisplay = _reference.GetDisplayText();
        List<string> wordDisplays = new List<string>();
        foreach (Word word in _words)
        {
            wordDisplays.Add(word.GetDisplayText());
        }
        string wordPart = string.Join(" ", wordDisplays);
        return string.IsNullOrEmpty(wordPart) ? refDisplay : refDisplay + " " + wordPart;
    }
}
