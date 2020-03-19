using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Converters
{
    internal static class HiraganaConverter
    {
        public static string ToHiragana(string input, WanaKanaOptions? options = null)
        {
            options ??= new WanaKanaOptions();

            if (options.PassRomaji)
            {
                return KatakanaConverter.KatakanaToHiragana(input);
            }

            if (MixedChecker.IsMixed(input))
            {
                var convertedKatakana = KatakanaConverter.KatakanaToHiragana(input);
                return ToKana(convertedKatakana.ToLowerInvariant(), options);
            }

            if (RomajiChecker.IsRomaji(input) || SpecialCharacterCheckers.IsEnglishPunctuation(input[0]))
            {
                return ToKana(input.ToLowerInvariant(), options);
            }

            return KatakanaConverter.KatakanaToHiragana(input);
        }

        public static string HiraganaToKatakana(string input)
        {
            return string.Empty;
        }
    }
}
