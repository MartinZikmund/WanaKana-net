using System.Text.RegularExpressions;
using WanaKanaNet.Checkers;
using WanaKanaNet.Converters;
using WanaKanaNet.Helpers;

namespace WanaKanaNet
{
    /// <summary>
    /// Contains all checkers and converters provided by WanaKana.
    /// </summary>
    public static class WanaKana
    {
        /// <summary>
        /// Test if input is hiragana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is hiragana.</returns>
        public static bool IsHiragana(char input) => HiraganaChecker.IsHiragana(input);

        /// <summary>
        /// Test if input is hiragana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is hiragana.</returns>
        public static bool IsHiragana(string input) => HiraganaChecker.IsHiragana(input);

        /// <summary>
        /// Test if input is katakana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is katakana.</returns>
        public static bool IsKatakana(char input) => KatakanaChecker.IsKatakana(input);

        /// <summary>
        /// Test if input is katakana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is katakana.</returns>
        public static bool IsKatakana(string input) => KatakanaChecker.IsKatakana(input);

        /// <summary>
        /// Test if input only includes Kanji, Kana, zenkaku numbers, and JA punctuation/symbols.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is japanese.</returns>
        public static bool IsJapanese(char input) => JapaneseChecker.IsJapanese(input);

        /// <summary>
        /// Test if input only includes Kanji, Kana, zenkaku numbers, and JA punctuation/symbols.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="additionalCharacters">Additional allowed characters.</param>
        /// <returns>A value indicating whether input is japanese.</returns>
        public static bool IsJapanese(string input, params char[] additionalCharacters) => JapaneseChecker.IsJapanese(input, additionalCharacters);

        /// <summary>
        /// Test if input only includes Kanji, Kana, zenkaku numbers, and JA punctuation/symbols.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="additionalCharactersRegex">Additional allowed characters regex.</param>
        /// <returns>A value indicating whether input is japanese.</returns>
        public static bool IsJapanese(string input, Regex additionalCharactersRegex) => JapaneseChecker.IsJapanese(input, additionalCharactersRegex);

        /// <summary>
        /// Test if input is kana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is kana.</returns>
        public static bool IsKana(char input) => KanaChecker.IsKana(input);

        /// <summary>
        /// Test if input is kana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is kana.</returns>
        public static bool IsKana(string input) => KanaChecker.IsKana(input);

        /// <summary>
        /// Test if input is kanji.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is kanji.</returns>
        public static bool IsKanji(char input) => KanjiChecker.IsKanji(input);

        /// <summary>
        /// Test if input is kanji.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is kanji.</returns>
        public static bool IsKanji(string input) => KanjiChecker.IsKanji(input);

        /// <summary>
        /// Test if input contains a mix of Romaji and Kana, defaults to pass through Kanji.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="passKanji">A value indicating whether the checker should pass through kanji.</param>
        /// <returns>A value indicating whether input is mixed.</returns>
        public static bool IsMixed(string input, bool passKanji = true) => MixedChecker.IsMixed(input, passKanji);

        /// <summary>
        /// Test if input is Romaji (allowing Hepburn romanisation).
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is romaji.</returns>
        public static bool IsRomaji(char input) => RomajiChecker.IsRomaji(input);

        /// <summary>
        /// Test if input is Romaji (allowing Hepburn romanisation).
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>A value indicating whether input is romaji.</returns>
        public static bool IsRomaji(string input) => RomajiChecker.IsRomaji(input);

        /// <summary>
        /// Test if input is Romaji (allowing Hepburn romanisation).
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="additionalCharactersRegex">Additional allowed characters.</param>
        /// <returns>A value indicating whether input is romaji.</returns>
        public static bool IsRomaji(string input, params char[] additionalCharacters) => RomajiChecker.IsRomaji(input, additionalCharacters);

        /// <summary>
        /// Test if input is Romaji (allowing Hepburn romanisation).
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="additionalCharactersRegex">Regex with additional allowed characters.</param>
        /// <returns>A value indicating whether input is romaji.</returns>
        public static bool IsRomaji(string input, Regex additionalCharactersRegex) => RomajiChecker.IsRomaji(input, additionalCharactersRegex);

        /// <summary>
        /// Strips Okurigana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <returns>Input without okurigana.</returns>
        public static string StripOkurigana(string input) => OkuriganaHelpers.StripOkurigana(input);

        /// <summary>
        /// Convert input to Hiragana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="options">Options.</param>
        /// <returns>Hiragana.</returns>
        public static string ToHiragana(string input, WanaKanaOptions? options = null) => HiraganaConverter.ToHiragana(input, options);

        /// <summary>
        /// Convert input to Katakana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="options">Options.</param>
        /// <returns>Katakana.</returns>
        public static string ToKatakana(string input, WanaKanaOptions? options = null) => KatakanaConverter.ToKatakana(input, options);

        /// <summary>
        /// Convert Romaji to Kana, lowercase text will result in Hiragana and uppercase text will result in Katakana.
        /// </summary>
        /// <param name="input">Input.</param>
        /// <param name="options">Options.</param>
        /// <returns>Kana.</returns>
        public static string ToKana(string input, WanaKanaOptions? options = null) => KanaConverters.ToKana(input, options);

        /// <summary>
        /// Convert kana to romaji.
        /// </summary>
        /// <param name="input">Kana or mixed input.</param>
        /// <param name="options">Options.</param>
        /// <returns>Romaji.</returns>
        public static string ToRomaji(string input, WanaKanaOptions? options = null) => RomajiConverter.ToRomaji(input, options);

        /// <summary>
        /// Splits input into array of strings separated by opinionated token types 
        /// 'en', 'ja', 'englishNumeral', 'japaneseNumeral','englishPunctuation', 
        /// 'japanesePunctuation','kanji', 'hiragana', 'katakana', 'space', 'other'. 
        /// If { compact: true } then many same-language tokens are combined 
        /// (spaces + text, kanji + kana, numeral + punctuation).       
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="compact">Value indicating whether tokenizer should compact same-type tokens</param>
        /// <returns>Input broken down to tokens.</returns>
        public static Token[] Tokenize(string input, bool compact = false) => Tokenizer.Tokenize(input, compact);
    }
}
