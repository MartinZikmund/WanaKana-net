using System;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Converters
{
    internal static class HiraganaConverter
    {
        public static string ToHiragana(string input, WanaKanaOptions? options = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            options ??= new WanaKanaOptions();

            if (input.Length == 0) return string.Empty;

            if (options.PassRomaji)
            {
                return KanaConverters.KatakanaToHiragana(input);
            }

            if (MixedChecker.IsMixed(input))
            {
                var convertedKatakana = KanaConverters.KatakanaToHiragana(input);
                return KanaConverters.ToKana(convertedKatakana.ToLowerInvariant(), options);
            }

            if (RomajiChecker.IsRomaji(input) || SpecialCharacterChecker.IsEnglishPunctuation(input[0]))
            {
                return KanaConverters.ToKana(input.ToLowerInvariant(), options);
            }

            return KanaConverters.KatakanaToHiragana(input);
        }
    }
}
