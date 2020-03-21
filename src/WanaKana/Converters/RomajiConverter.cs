using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
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
                throw new System.ArgumentNullException(nameof(input));
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
                builder.Append(makeUppercase ? romaji.ToUpperInvariant() : romaji);
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

            return TrieHelpers.ApplyTrie(KatakanaConverter.KatakanaToHiragana(input, true), map, options.ImeMode == Enums.ImeMode.None);
        }
    }
}
