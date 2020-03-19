using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WanaKanaNet.Characters
{
    internal static class RomajiTokanaMap
    {
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

        public void CreateRomajiToKanaMap()
        {
            var kanaTree = Transform(Basic)
        }
    }
}
