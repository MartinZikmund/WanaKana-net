using System;
using System.Collections.Generic;
using WanaKanaNet.Mapping;

namespace WanaKanaNet.Characters
{
    internal static class KanaToRomajiMap
    {
        private static Trie? _kanaToHepburnMap = null;

        private static readonly IReadOnlyDictionary<string, string> BasicRomaji = new Dictionary<string, string>
        {
          {"あ","a"},{"い","i"},{"う","u"},{"え","e"},{"お","o"},
          {"か","ka"},{"き","ki"},{"く","ku"},{"け","ke"},{"こ","ko"},
          {"さ","sa"},{"し","shi"},{"す","su"},{"せ","se"},{"そ","so"},
          {"た","ta"},{"ち","chi"},{"つ","tsu"},{"て","te"},{"と","to"},
          {"な","na"},{"に","ni"},{"ぬ","nu"},{"ね","ne"},{"の","no"},
          {"は","ha"},{"ひ","hi"},{"ふ","fu"},{"へ","he"},{"ほ","ho"},
          {"ま","ma"},{"み","mi"},{"む","mu"},{"め","me"},{"も","mo"},
          {"ら","ra"},{"り","ri"},{"る","ru"},{"れ","re"},{"ろ","ro"},
          {"や","ya"},{"ゆ","yu"},{"よ","yo"},
          {"わ","wa"},{"ゐ","wi"},{"ゑ","we"},{"を","wo"},
          {"ん", "n"},
          {"が","ga"},{"ぎ","gi"},{"ぐ","gu"},{"げ","ge"},{"ご","go"},
          {"ざ","za"},{"じ","ji"},{"ず","zu"},{"ぜ","ze"},{"ぞ","zo"},
          {"だ","da"},{"ぢ","ji"},{"づ","zu"},{"で","de"},{"ど","do"},
          {"ば","ba"},{"び","bi"},{"ぶ","bu"},{"べ","be"},{"ぼ","bo"},
          {"ぱ","pa"},{"ぴ","pi"},{"ぷ","pu"},{"ぺ","pe"},{"ぽ","po"},
          {"ゔぁ","va"},{"ゔぃ","vi"},{"ゔ","vu"},{"ゔぇ","ve"},{"ゔぉ","vo"},
        };

        private static readonly IDictionary<char, char> SpecialSymbols = new Dictionary<char, char>
        {
            {'。','.'},
            {'、',','},
            {'：',':'},
            {'・','/'},
            {'！','!'},
            {'？','?'},
            {'〜','~'},
            {'ー','-'},
            {'「','‘'},
            {'」','’'},
            {'『','“'},
            {'』','”'},
            {'［','['},
            {'］',']'},
            {'（','('},
            {'）',')'},
            {'｛','{'},
            {'｝','}'},
            {'　',' '},
        };

        // んい -> n'i
        private static readonly char[] AmbiguousVowels = { 'あ', 'い', 'う', 'え', 'お', 'や', 'ゆ', 'よ' };

        private static readonly IDictionary<string, string> SmallY = new Dictionary<string, string>() { { "ゃ", "ya" }, { "ゅ", "yu" }, { "ょ", "yo" } };

        private static readonly IDictionary<string, string> SmallYExtra = new Dictionary<string, string>() { { "ぃ", "yi" }, { "ぇ", "ye" } };

        private static readonly IDictionary<char, char> SmallAiueo = new Dictionary<char, char>()
        {
            {'ぁ','a'},
            {'ぃ','i'},
            {'ぅ','u'},
            {'ぇ','e'},
            {'ぉ','o'},
        };

        private static readonly char[] YoonKana = { 'き', 'に', 'ひ', 'み', 'り', 'ぎ', 'び', 'ぴ', 'ゔ', 'く', 'ふ' };

        private static readonly IDictionary<string, string> YoonExceptions = new Dictionary<string, string>() {
            {"し","sh"},
            {"ち","ch"},
            {"じ","j"},
            {"ぢ","j"},
        };

        private static readonly IDictionary<string, string> SmallKana = new Dictionary<string, string>()
        {
            {"っ",""},
            {"ゃ","ya"},
            {"ゅ","yu"},
            {"ょ","yo"},
            {"ぁ","a"},
            {"ぃ","i"},
            {"ぅ","u"},
            {"ぇ","e"},
            {"ぉ","o"},
        };

        // going with the intuitive (yet incorrect) solution where っや -> yya and っぃ -> ii
        // in other words, just assume the sokuon could have been applied to anything
        private static readonly IDictionary<char, char> SokuonWhitelist = new Dictionary<char, char>()
        {
          {'b','b'},
          {'c','t'},
          {'d','d'},
          {'f','f'},
          {'g','g'},
          {'h','h'},
          {'j','j'},
          {'k','k'},
          {'m','m'},
          {'p','p'},
          {'q','q'},
          {'r','r'},
          {'s','s'},
          {'t','t'},
          {'v','v'},
          {'w','w'},
          {'x','x'},
          {'z','z' },
        };

        public static Trie GetKanaToRomajiTree(WanaKanaOptions options)
        {
            switch (options.Romanization)
            {
                case RomanizationType.Hepburn:
                    return GetKanaToHepburnTree();
                default:
                    throw new ArgumentOutOfRangeException(nameof(options), "This type of romanization is not supported");
            }
        }

        private static Trie GetKanaToHepburnTree() => _kanaToHepburnMap ??= CreateKanaToHepburnMap();

        private static Trie CreateKanaToHepburnMap()
        {
            var romajiTree = Trie.FromDictionary(BasicRomaji);

            foreach (var symbol in SpecialSymbols)
            {
                romajiTree[symbol.Key.ToString()] = symbol.Value.ToString();
            }

            foreach (var y in SmallY)
            {
                romajiTree[y.Key] = y.Value;
            }

            foreach (var aiueo in SmallAiueo)
            {
                romajiTree[aiueo.Key.ToString()] = aiueo.Value.ToString();
            }

            // きゃ -> kya
            foreach (var yoonKana in YoonKana)
            {
                var firstRomajiChar = romajiTree[yoonKana.ToString()]![0];
                foreach (var y in SmallY)
                {
                    romajiTree[yoonKana + y.Key] = firstRomajiChar + y.Value;
                }
            }

            foreach (var yoonException in YoonExceptions)
            {
                var kana = yoonException.Key;
                var roma = yoonException.Value;
                // じゃ -> ja
                foreach (var y in SmallY)
                {
                    var yKana = y.Key;
                    var yRoma = y.Value;
                    romajiTree[kana + yKana] = roma + yRoma[1];
                }
                // じぃ -> jyi, じぇ -> je
                romajiTree[$"{kana}ぃ"] = $"{roma}yi";
                romajiTree[$"{kana}ぇ"] = $"{roma}e";
            }

            romajiTree.InsertSubtrie("っ", ResolveTsu(romajiTree));

            foreach (var kana in AmbiguousVowels)
            {
                romajiTree[$"ん{kana}"] = $"n'{romajiTree[kana.ToString()]}";
            }

            // NOTE: could be re-enabled with an option?
            // // んば -> mbo
            // const LABIAL = [
            //   'ば', 'び', 'ぶ', 'べ', 'ぼ',
            //   'ぱ', 'ぴ', 'ぷ', 'ぺ', 'ぽ',
            //   'ま', 'み', 'む', 'め', 'も',
            // ];
            // LABIAL.forEach((kana) => {
            //   setTrans(`ん${kana}`, `m${subtreeOf(kana)['']}`);
            // });

            return romajiTree;
        }

        private static Trie ResolveTsu(Trie trie)
        {
            var tsuTrie = new Trie();
            foreach (var entry in trie.GetEntries())
            {
                var consonant = entry.Value[0];
                tsuTrie[entry.Key] = SokuonWhitelist.ContainsKey(consonant)
                      ? SokuonWhitelist[consonant] + entry.Value
                      : entry.Value;
            }
            return tsuTrie;
        }
    }
}
