using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class JapaneseChecker
    {
        public static bool IsJapanese(string input, params char[] additionalAllowedChars)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;

            return input.All(c => IsJapanese(c) || additionalAllowedChars.Contains(c));
        }

        public static bool IsJapanese(string input, Regex additionalCharactersRegex)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;

            return input.All(c => IsJapanese(c) || additionalCharactersRegex.IsMatch(c.ToString()));
        }

        public static bool IsJapanese(char character) =>
            CharacterBounds.JapaneseRanges.Any(range => range.Contains(character));
    }
}
