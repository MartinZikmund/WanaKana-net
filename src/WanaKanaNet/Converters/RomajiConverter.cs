﻿using System;
using System.Text;
using WanaKanaNet.Characters;
using WanaKanaNet.Checkers;
using WanaKanaNet.Helpers;
using WanaKanaNet.Mapping;

namespace WanaKanaNet.Converters
{
    internal static class RomajiConverter
    {
        public static string ToRomaji(string input, WanaKanaOptions? options = null)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            options ??= new WanaKanaOptions();

            if (input.Length == 0) return string.Empty;

            // just throw away the substring index information and just concatenate all the kana
            var romajiTokens = SplitIntoRomaji(input, options);
            var builder = new StringBuilder();
            foreach (var token in romajiTokens)
            {
                var (start, end, romaji) = token;
                var makeUppercase = options.UppercaseKatakana && KatakanaChecker.IsKatakana(input.Substring(start, end - start));
                builder.Append(makeUppercase ? romaji?.ToUpperInvariant() ?? string.Empty : romaji);
            }
            return builder.ToString();
        }
        
        private static SplitToken[] SplitIntoRomaji(string input, WanaKanaOptions options)
        {
            var map = KanaToRomajiMap.GetKanaToRomajiTree(options);

            if (options.CustomRomajiMapping != null)
            {

                map = map.Clone();
                map.AddRange(options.CustomRomajiMapping);                
            }

            return TrieHelpers.ApplyTrie(KanaConverters.KatakanaToHiragana(input, true), map, options.ImeMode == ImeMode.None);
        }
    }
}
