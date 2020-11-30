using System;
using System.Collections.Generic;

namespace AutomoderatorGameBot.BackEnd.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceFirst(this string text, string stringToReplace, string replacementStringValue)
        {
            var indexOfFoundText = text.IndexOf(stringToReplace, StringComparison.Ordinal);
            if (indexOfFoundText < 0)
                return text;
            return text.Substring(0, indexOfFoundText) + replacementStringValue +
                   text.Substring(indexOfFoundText + stringToReplace.Length);
        }

        public static string ReplaceLastOccurrence(this string text, string stringToReplace,
            string replacementStringValue)
        {
            var place = text.LastIndexOf(stringToReplace, StringComparison.Ordinal);

            if (place == -1)
                return text;

            var result = text.Remove(place, stringToReplace.Length).Insert(place, replacementStringValue);
            return result;
        }
        
        /// <summary>
        /// Jon Skeet's method of splitting strings into chunks.
        /// See: https://stackoverflow.com/questions/1632078/split-string-in-512-char-chunks
        /// </summary>
        /// <param name="text">Text to split up</param>
        /// <param name="chunkSize">Size of chunks</param>
        /// <returns>text split into an IList with chunks of chunkSize</returns>
        public static IList<string> SplitIntoChunks(this string text, int chunkSize)
        {
            var chunks = new List<string>();
            var offset = 0;
            while (offset < text.Length)
            {
                var size = Math.Min(chunkSize, text.Length - offset);
                chunks.Add(text.Substring(offset, size));
                offset += size;
            }
            return chunks;
        }

    }
}