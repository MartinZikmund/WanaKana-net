using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WanaKanaNet.Characters;
using WanaKanaNet.Checkers;
using WanaKanaNet.Helpers;
using WanaKanaNet.Mapping;

namespace WanaKanaNet.Converters
{
    internal static class KanaConverters
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

        public static string ToKana(string input, WanaKanaOptions? options = null, IReadOnlyDictionary<string, string>? map = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            options ??= new WanaKanaOptions();

            if (input.Length == 0) return string.Empty;

            Trie trie;
            if (map == null)
            {
                trie = CreateRomajiToKanaMap(options);
            }
            else
            {
                throw new NotImplementedException();
            }

            var tokens = SplitIntoConvertedKana(input, options, trie);
            var builder = new StringBuilder();
            foreach (var token in tokens)
            {
                var (start, end, kana) = token;
                if (kana == null)
                {
                    // haven't converted the end of the string, since we are in IME mode
                    builder.Append(input.Substring(start));
                }
                else
                {
                    var enforceHiragana = options.ImeMode == ImeMode.ToHiragana;
                    var enforceKatakana = options.ImeMode == ImeMode.ToKatakana || input.Substring(start, end - start).All(char.IsUpper);
                    builder.Append(enforceHiragana || !enforceKatakana ? kana : HiraganaToKatakana(kana));
                }
            }
            return builder.ToString();
        }

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
                    else
                    {
                        builder.Append(_longVowels[romaji]);
                    }
                }
                else if (!SpecialCharacterChecker.IsLongDash(character) && KatakanaChecker.IsKatakana(character))
                {
                    // Shift charcode.
                    var hiraganaCode = (character - CharacterRanges.KatakanaStart) + CharacterRanges.HiraganaStart;
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

        public static string HiraganaToKatakana(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var builder = new StringBuilder();
            foreach (var character in input)
            {
                // Short circuit to avoid incorrect codeshift for 'ー' and '・'
                if (SpecialCharacterChecker.IsLongDash(character) || SpecialCharacterChecker.IsSlashDot(character))
                {
                    builder.Append(character);
                }
                else if (HiraganaChecker.IsHiragana(character))
                {
                    // Shift charcode.
                    var katakanaCode = (character - CharacterRanges.HiraganaStart) + CharacterRanges.KatakanaStart;
                    var katakanaCharacter = (char)katakanaCode;
                    builder.Append(katakanaCharacter);
                }
                else
                {
                    // Pass non-hiragana chars through
                    builder.Append(character);
                }
            }
            return builder.ToString();
        }

        private static bool IsInitialLongDash(char character, int index) => SpecialCharacterChecker.IsLongDash(character) && index == 0;

        private static bool IsInnerLongDash(char character, int index) => SpecialCharacterChecker.IsLongDash(character) && index > 0;

        private static bool IsKanaAsSymbol(char character) => _symbolKana.Contains(character);

        private static Trie CreateRomajiToKanaMap(WanaKanaOptions options)
        {
            var map = RomajiToKanaMap.GetRomajiToKanaTree().Clone();

            if (options.ImeMode != ImeMode.None)
            {
                RomajiToKanaMap.ApplyImeModeMap(map);
            }
            if (options.UseObsoleteKana)
            {
                RomajiToKanaMap.ApplyObsoleteKanaMap(map);
            }

            if (options.CustomKanaMapping != null)
            {
                map.AddRange(options.CustomKanaMapping);
            }

            return map;
        }

        private static SplitToken[] SplitIntoConvertedKana(string input, WanaKanaOptions? options, Trie trie)
        {
            options ??= new WanaKanaOptions();
            if (trie == null)
            {
                trie = CreateRomajiToKanaMap(options);
            }

            return TrieHelpers.ApplyTrie(input.ToLowerInvariant(), trie, options.ImeMode == ImeMode.None);
        }
    }
}
