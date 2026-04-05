using System;

public class PlaceholderSlot
{
    private string _placeholderType;
    private string _userWord;
    private int _orderIndex;
    private string _exactMarker;

    public PlaceholderSlot(string type, int orderIndex, string exactMarkerInText)
    {
        _placeholderType = NormalizeType(type ?? "");
        _userWord = "";
        _orderIndex = orderIndex;
        _exactMarker = string.IsNullOrEmpty(exactMarkerInText)
            ? "{" + _placeholderType + "}"
            : exactMarkerInText;
    }

    private static string NormalizeType(string type)
    {
        string t = type.Trim();
        string u = t.ToUpperInvariant();
        if (u == "ADJECTIVE")
            return "ADJ";
        return u;
    }

    public string GetPlaceholderType()
    {
        return _placeholderType;
    }

    public string GetExactMarker()
    {
        return _exactMarker;
    }

    public string GetUserWord()
    {
        return _userWord;
    }

    public void SetUserWord(string word)
    {
        _userWord = word ?? "";
    }

    public int GetOrderIndex()
    {
        return _orderIndex;
    }
}
