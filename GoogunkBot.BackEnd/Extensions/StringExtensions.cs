using System;

namespace GoogunkBot.BackEnd.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceFirst(this string text, string stringToReplace, string replacementStringValue)
        {
            var indexOfFoundText = text.IndexOf(stringToReplace, StringComparison.Ordinal);
            if (indexOfFoundText < 0)
                return text;
            return text.Substring(0, indexOfFoundText) + replacementStringValue + text.Substring(indexOfFoundText + stringToReplace.Length);
        }
        
        public static string ReplaceLastOccurrence(this string text, string stringToReplace, string replacementStringValue)
        {
            var place = text.LastIndexOf(stringToReplace, StringComparison.Ordinal);

            if(place == -1)
                return text;

            var result = text.Remove(place, stringToReplace.Length).Insert(place, replacementStringValue);
            return result;
        }
    }
}