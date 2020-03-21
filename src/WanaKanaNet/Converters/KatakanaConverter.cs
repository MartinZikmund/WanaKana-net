using System;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Converters
{
    internal static class KatakanaConverter
    {        
        public static string ToKatakana(string input, WanaKanaOptions? options = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            options ??= new WanaKanaOptions();

            if (input.Length == 0) return string.Empty;

            if (options.PassRomaji)
            {
                return KanaConverters.HiraganaToKatakana(input);
            }

            if (MixedChecker.IsMixed(input) || RomajiChecker.IsRomaji(input) || SpecialCharacterChecker.IsEnglishPunctuation(input[0]))
            {
                var hiragana = KanaConverters.ToKana(input.ToLowerInvariant(), options);
                return KanaConverters.HiraganaToKatakana(hiragana);
            }

            return KanaConverters.HiraganaToKatakana(input);
        }
    }
}
