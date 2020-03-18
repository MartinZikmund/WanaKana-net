using System;
using System.Linq;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class HiraganaChecker
    {
        public static bool IsHiragana(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;
            return input.All(IsHiragana);
        }

        public static bool IsHiragana(char character)
        {
            if (SpecialCharacterCheckers.IsLongDash(character)) return true;
            return CharacterBounds.HiraganaChars.Contains(character);
        }
    }
}
