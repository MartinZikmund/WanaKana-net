using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class KanaChecker
    {
        public static bool IsKana(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;
            return input.All(IsKana);
        }

        public static bool IsKana(char character)
        {
            if (SpecialCharacterCheckers.IsLongDash(character)) return true;
            return HiraganaChecker.IsHiragana(character) || KatakanaChecker.IsKatakana(character);
        }
    }
}
