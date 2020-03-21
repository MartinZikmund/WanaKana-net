using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WanaKanaNet.Mapping;

namespace WanaKanaNet.Characters
{
    internal static class RomajiToKanaMap
    {
        private static Trie? _romajiToKanaTree = null;

        private static readonly IReadOnlyDictionary<string, string> BasicKunrei = new Dictionary<string, string>()
        {
            {"a","あ"}, {"i", "い"}, {"u","う"}, {"e","え"}, {"o", "お"},
            {"ka", "か"}, {"ki","き"}, {"ku","く"}, {"ke","け"}, {"ko","こ"},
            {"sa", "さ"}, {"si","し"}, {"su","す"}, {"se","せ"}, {"so","そ"},
            {"ta", "た"}, {"ti","ち"}, {"tu","つ"}, {"te","て"}, {"to","と"},
            {"na", "な"}, {"ni","に"}, {"nu","ぬ"}, {"ne","ね"}, {"no","の"},
            {"ha", "は"}, {"hi","ひ"}, {"hu","ふ"}, {"he","へ"}, {"ho","ほ"},
            {"ma", "ま"}, {"mi","み"}, {"mu","む"}, {"me","め"}, {"mo","も"},
            {"ya", "や"}, {"yu","ゆ"}, {"yo","よ"},
            {"ra", "ら"}, {"ri","り"}, {"ru","る"}, {"re","れ"}, {"ro","ろ"},
            {"wa", "わ"}, {"wi","ゐ"}, {"we","ゑ"}, {"wo","を"},
            {"ga", "が"}, {"gi","ぎ"}, {"gu","ぐ"}, {"ge","げ"}, {"go","ご"},
            {"za", "ざ"}, {"zi","じ"}, {"zu","ず"}, {"ze","ぜ"}, {"zo","ぞ"},
            {"da", "だ"}, {"di","ぢ"}, {"du","づ"}, {"de","で"}, {"do","ど"},
            {"ba", "ば"}, {"bi","び"}, {"bu","ぶ"}, {"be","べ"}, {"bo","ぼ"},
            {"pa", "ぱ"}, {"pi","ぴ"}, {"pu","ぷ"}, {"pe","ぺ"}, {"po","ぽ"},
            {"va", "ゔぁ"}, {"vi","ゔぃ"}, {"vu","ゔ"}, {"ve","ゔぇ"}, {"vo","ゔぉ"},
        };

        public static readonly IReadOnlyDictionary<char, char> SpecialSymbols = new Dictionary<char, char>()
        {
            {'.', '。'},
            {',', '、'},
            {':', '：'},
            {'/', '・'},
            {'!', '！'},
            {'?', '？'},
            {'~', '〜'},
            {'-', 'ー'},
            {'‘', '「'},
            {'’', '」'},
            {'“', '『'},
            {'”', '』'},
            {'[', '［'},
            {']', '］'},
            {'(', '（'},
            {')', '）'},
            {'{', '｛'},
            {'}', '｝'},
        };

        public static readonly IReadOnlyDictionary<char, char> Consonants = new Dictionary<char, char>()
        {
            {'k', 'き'},
            {'s', 'し'},
            {'t', 'ち'},
            {'n', 'に'},
            {'h', 'ひ'},
            {'m', 'み'},
            {'r', 'り'},
            {'g', 'ぎ'},
            {'z', 'じ'},
            {'d', 'ぢ'},
            {'b', 'び'},
            {'p', 'ぴ'},
            {'v', 'ゔ'},
            {'q', 'く'},
            {'f', 'ふ' },
        };

        public static readonly IReadOnlyDictionary<string, string> SmallY = new Dictionary<string, string>()
        {
            { "ya", "ゃ" },
            { "yi", "ぃ" },
            { "yu", "ゅ" },
            { "ye", "ぇ" },
            { "yo", "ょ" },
        };

        public static readonly IReadOnlyDictionary<string, string> SmallVowels = new Dictionary<string, string>()
        {
            { "a", "ぁ" },
            { "i", "ぃ" },
            { "u", "ぅ" },
            { "e", "ぇ" },
            { "o", "ぉ" }
        };

        /// <summary>
        /// Typing one should be the same as having typed the other instead.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> Aliases = new Dictionary<string, string>()
        {
            {"sh", "sy"}, // sha -> sya
            {"ch", "ty"}, // cho -> tyo
            {"cy", "ty"}, // cyo -> tyo
            {"chy", "ty"}, // chyu -> tyu
            {"shy", "sy"}, // shya -> sya
            {"j", "zy"}, // ja -> zya
            {"jy", "zy"}, // jye -> zye

            // exceptions to above rules
            {"shi", "si"},
            {"chi", "ti"},
            {"tsu", "tu"},
            {"ji", "zi"},
            {"fu", "hu"},
        };

        /// <summary>
        /// xtu -> っ
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string> SmallLetters = new Dictionary<string, string>()
            {
                {"tu", "っ" },
                {"wa", "ゎ" },
                {"ka", "ヵ" },
                {"ke", "ヶ" },
            }
            .Union(SmallVowels)
            .Union(SmallY)
            .ToDictionary(kv => kv.Key, kv => kv.Value);

        public static readonly IReadOnlyDictionary<string, string> SpecialCases = new Dictionary<string, string>()
        {
            {"yi", "い"},
            {"wu", "う"},
            {"ye", "いぇ"},
            {"wi", "うぃ"},
            {"we", "うぇ"},
            {"kwa", "くぁ"},
            {"whu", "う"},
            // because it"s not thya for てゃ but tha
            // and tha is not てぁ, but てゃ
            {"tha", "てゃ"},
            {"thu", "てゅ"},
            {"tho", "てょ"},
            {"dha", "でゃ"},
            {"dhu", "でゅ"},
            {"dho", "でょ"},
        };

        public static readonly IReadOnlyDictionary<string, string> AiueoConstructions = new Dictionary<string, string>()
        {
            {"wh", "う"},
            {"qw", "く"},
            {"q", "く"},
            {"gw", "ぐ"},
            {"sw", "す"},
            {"ts", "つ"},
            {"th", "て"},
            {"tw", "と"},
            {"dh", "で"},
            {"dw", "ど"},
            {"fw", "ふ"},
            {"f", "ふ"},
        };

        public static Trie GetRomajiToKanaTree() => _romajiToKanaTree ??= CreateRomajiToKanaMap();

        private static Trie CreateRomajiToKanaMap()
        {
            string[] GetAlternatives(string input)
            {
                var results = new List<string>();
                foreach (var alias in Aliases.Union(new[] { new KeyValuePair<string, string>("c", "k") }))
                {
                    var alt = alias.Key;
                    var roma = alias.Value;
                    if (input.StartsWith(roma))
                    {
                        results.Add(input.Replace(roma, alt));
                    }
                }
                return results.ToArray();
            }

            var kanaTree = Trie.FromDictionary(BasicKunrei);

            foreach (var consonantPair in Consonants)
            {
                var consonant = consonantPair.Key;
                var yKana = consonantPair.Value;
                foreach (var smallY in SmallY)
                {
                    var roma = smallY.Key;
                    var kana = smallY.Value;

                    // for example kyo -> き + ょ
                    kanaTree[consonant + roma] = yKana + kana;
                }
            }

            foreach (var symbolPair in SpecialSymbols)
            {
                kanaTree[symbolPair.Key.ToString()] = symbolPair.Value.ToString();
            }

            // things like うぃ, くぃ, etc.
            foreach (var aiueoPair in AiueoConstructions)
            {
                var consonant = aiueoPair.Key;
                var aiueoKana = aiueoPair.Value;
                foreach (var vowelPair in SmallVowels)
                {
                    var vowel = vowelPair.Key;
                    var kana = vowelPair.Value;
                    kanaTree[consonant + vowel] = aiueoKana + kana;
                }
            }

            // different ways to write ん
            foreach (var nChar in new[] { "n", "n'", "xn" })
            {
                kanaTree[nChar] = "ん";
            }

            // c is equivalent to k, but not for chi, cha, etc. that's why we have to make a copy of k
            kanaTree.InsertSubtrie("c", kanaTree.GetSubtrie("k").Clone());

            foreach (var aliasPair in Aliases)
            {
                var source = aliasPair.Key;
                var alias = aliasPair.Value;

                var cut = source.Substring(0, source.Length - 1);
                var last = source[source.Length - 1];
                var parentTree = kanaTree.GetSubtrie(cut);
                parentTree.AssignSubtrie(last.ToString(), kanaTree.GetSubtrie(alias).Clone());
            }

            foreach (var smallLetterPair in SmallLetters)
            {
                var kunreiRoma = smallLetterPair.Key;
                var kana = smallLetterPair.Value;
                var xRoma = $"x{kunreiRoma}";
                var xSubtree = kanaTree.GetSubtrie(xRoma);
                xSubtree.Root.Value = kana;

                // ltu -> xtu -> っ
                var parentTree = kanaTree.GetSubtrie($"l{kunreiRoma.Substring(0, kunreiRoma.Length - 1)}");
                parentTree.AssignSubtrie(kunreiRoma[kunreiRoma.Length - 1].ToString(), xSubtree);

                // ltsu -> ltu -> っ
                var alternatives = GetAlternatives(kunreiRoma);
                foreach (var alternative in alternatives)
                {
                    foreach (var prefix in new char[] { 'l', 'x' })
                    {
                        var altParentTree = kanaTree.GetSubtrie(prefix + alternative.Substring(0, alternative.Length - 1));
                        altParentTree.AssignSubtrie(alternative[alternative.Length - 1].ToString(), kanaTree.GetSubtrie(prefix + kunreiRoma));
                    }
                }
            }

            foreach (var specialCase in SpecialCases)
            {
                kanaTree[specialCase.Key] = specialCase.Value;
            }

            Trie AddTsu(Trie tree)
            {
                var tsuTrie = new Trie();
                foreach (var entry in tree.GetEntries())
                {
                    // we have reached the bottom of this branch
                    tsuTrie[entry.Key] = $"っ{entry.Value}";
                }
                return tsuTrie;
            }

            // have to explicitly name c here, because we made it a copy of k, not a reference
            foreach (var consonant in Consonants.Keys.Union(new[] { 'c', 'y', 'w', 'j' }))
            {
                var subtree = kanaTree.GetSubtrie(consonant.ToString());
                subtree.InsertSubtrie(consonant.ToString(), AddTsu(subtree));
            }

            // nn should not be っん
            kanaTree.GetSubtrie("n").Root.DeleteChild('n');

            return kanaTree;
        }

        internal static void ApplyImeModeMap(Trie trie)
        {
            // in IME mode, we do not want to convert single ns            
            trie["nn"] = "ん";
            trie["n "] = "ん";
        }

        internal static void ApplyObsoleteKanaMap(Trie trie)
        {
            trie.AddRange(new Dictionary<string, string>() { { "wi", "ゐ" }, { "we", "ゑ" } });
        }
    }
}