using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;
using WanaKanaNet.Enums;
using WanaKanaNet.Helpers;
using WanaKanaNet.Mapping;

namespace WanaKanaNet.Converters
{
    internal static class KanaConverter
    {
        public static string ToKana(string input, WanaKanaOptions? options = null, IReadOnlyDictionary<string, string>? map = null)
        {
            options ??= new WanaKanaOptions();
            Trie trie;
            if (map == null)
            {
                trie = CreateRomajiToKanaMap(options);
            }
            else
            {
                throw new NotImplementedException();
            }

            return string.Join(string.Empty, SplitIntoConvertedKana(input, options, trie).Select(s => s.Content));
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
