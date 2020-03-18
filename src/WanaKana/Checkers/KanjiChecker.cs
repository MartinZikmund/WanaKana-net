using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class KanjiChecker
    {
        public static bool IsKanji(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (input == string.Empty) return false;
            return input.All(IsKanji);
        }

        public static bool IsKanji(char character) => CharacterBounds.KanjiRange.Contains(character);
    }
}
