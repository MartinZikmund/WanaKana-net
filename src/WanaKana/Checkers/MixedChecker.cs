using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Checkers
{
    internal static class MixedChecker
    {
        public static bool IsMixed(string input, bool passKanji = true)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var hasKanji = false;
            if (!passKanji)
            {
                hasKanji = input.Any(KanjiChecker.IsKanji);
            }

            var hasKana = input.Any(HiraganaChecker.IsHiragana) ||
                         input.Any(KatakanaChecker.IsKatakana);

            return hasKana && 
                   input.Any(RomajiChecker.IsRomaji) && 
                   !hasKanji;
        }
    }
}
