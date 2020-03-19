using System;
using System.Linq;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class KatakanaChecker
    {
        public static bool IsKatakana(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;
            return input.All(IsKatakana);
        }

        public static bool IsKatakana(char character)
        {
            if (SpecialCharacterChecker.IsLongDash(character)) return true;
            return CharacterBounds.KatakanaChars.Contains(character);
        }
    }
}
