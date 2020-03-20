using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Converters
{
    internal static class KatakanaConverter
    {
        private static readonly char[] _symbolKana = { 'ヶ', 'ヵ' };

        private static readonly IReadOnlyDictionary<char, char> _longVowels = new Dictionary<char, char>()
        {
            { 'a', 'あ' },
            { 'i', 'い' },
            { 'u', 'う' },
            { 'e', 'え' },
            { 'o', 'う' },
        };

        public static string KatakanaToHiragana(string input, bool isDestinationRomaji = false)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            char? previousKana = null;
            var builder = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                var character = input[index];

                // Short circuit to avoid incorrect codeshift for 'ー' and '・'
                if (SpecialCharacterChecker.IsSlashDot(character) || IsInitialLongDash(character, index) || IsKanaAsSymbol(character))
                {
                    builder.Append(character);
                    // Transform long vowels: 'オー' to 'おう'
                }
                else if (previousKana != null && IsInnerLongDash(character, index))
                {
                    // Transform previousKana back to romaji, and slice off the vowel
                    var romaji = RomajiConverter.ToRomaji(previousKana.ToString()).Last();
                    // However, ensure 'オー' => 'おお' => 'oo' if this is a transform on the way to romaji
                    if (KatakanaChecker.IsKatakana(input[index - 1]) && romaji == 'o' && isDestinationRomaji)
                    {
                        builder.Append('お');
                    }
                    builder.Append(_longVowels[romaji]);
                }
                else if (!SpecialCharacterChecker.IsLongDash(character) && KatakanaChecker.IsKatakana(character))
                {
                    // Shift charcode.
                    var hiraganaCode = (character - CharacterBounds.KatakanaStart) + CharacterBounds.HiraganaStart;
                    var hiragana = (char)hiraganaCode;
                    previousKana = hiragana;
                    builder.Append(hiragana);
                }
                else
                {
                    // Pass non katakana chars through
                    previousKana = null;
                    builder.Append(character);
                }
            }
            return builder.ToString();
        }

        public static string ToKatakana(string input, WanaKanaOptions? options = null)
        {
            options ??= new WanaKanaOptions();
            if (options.PassRomaji)
            {
                return HiraganaConverter.HiraganaToKatakana(input);
            }

            if (MixedChecker.IsMixed(input) || RomajiChecker.IsRomaji(input) || SpecialCharacterChecker.IsEnglishPunctuation(input[0]))
            {
                var hiragana = KanaConverter.ToKana(input.ToLowerInvariant(), options);
                return HiraganaConverter.HiraganaToKatakana(hiragana);
            }

            return HiraganaConverter.HiraganaToKatakana(input);
        }

        private static bool IsInitialLongDash(char character, int index) => SpecialCharacterChecker.IsLongDash(character) && index == 0;

        private static bool IsInnerLongDash(char character, int index) => SpecialCharacterChecker.IsLongDash(character) && index > 0;

        private static bool IsKanaAsSymbol(char character) => _symbolKana.Contains(character);
    }
}
