using System;
using System.Linq;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        var delimiters = new[] { ' ', '-', '_' };
        var words = phrase.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(w => char.ToUpper(w[0])));
    }
}