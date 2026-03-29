using System;

public class PlaceholderSlot
{
    private string _placeholderType;
    private string _userWord;
    private int _orderIndex;

    public PlaceholderSlot(string type, int orderIndex)
    {
        _placeholderType = type ?? "";
        _userWord = "";
        _orderIndex = orderIndex;
    }

    public string GetPlaceholderType()
    {
        return _placeholderType;
    }

    public string GetUserWord()
    {
        return _userWord;
    }

    public void SetUserWord(string word)
    {
        _userWord = word ?? "";
    }

    public bool IsFilled()
    {
        return !string.IsNullOrWhiteSpace(_userWord);
    }

    public int GetOrderIndex()
    {
        return _orderIndex;
    }
}
