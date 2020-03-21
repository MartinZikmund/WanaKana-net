using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;
using WanaKanaNet.Checkers;
using WanaKanaNet.Enums;
using WanaKanaNet.Helpers;
using WanaKanaNet.Mapping;

namespace WanaKanaNet.Converters
{
    internal static class KanaConverter
    {
        public static string ToKana(string input, WanaKanaOptions? options = null, IReadOnlyDictionary<string, string>? map = null)
        {
            if (input is null)
            {
                throw new System.ArgumentNullException(nameof(input));
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
                var enforceHiragana = options.ImeMode == ImeMode.ToHiragana;
                var enforceKatakana = options.ImeMode == ImeMode.ToKatakana || input.Substring(start,end - start).All(char.IsUpper);
                builder.Append(enforceHiragana || !enforceKatakana ? kana : HiraganaConverter.HiraganaToKatakana(kana));
            }
            return builder.ToString();
        }

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
