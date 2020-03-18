using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class RomajiChecker
    {
        public static bool IsRomaji(string input, params char[] additionalAllowedChars)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;

            return input.All(c => IsRomaji(c) || additionalAllowedChars.Contains(c));
        }

        public static bool IsRomaji(string input, Regex additionalCharactersRegex)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;

            return input.All(c => IsRomaji(c) || additionalCharactersRegex.IsMatch(c.ToString()));
        }

        public static bool IsRomaji(char character) =>
            CharacterBounds.RomajiRanges.Any(range => range.Contains(character));
    }
}
