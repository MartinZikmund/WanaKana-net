using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Helpers
{
    internal static class OkuriganaHelper
    {
        public static string StripOkurigana(string input, bool isLeading = false, params char[] matchKanji)
        {
            if (matchKanji is null)
            {
                throw new ArgumentNullException(nameof(matchKanji));
            }
            
            if (
                !JapaneseChecker.IsJapanese(input) ||
                IsLeadingWithoutInitialKana(input, isLeading) ||
                IsTrailingWithoutFinalKana(input, isLeading) ||
                IsInvalidMatcher(input, matchKanji))
            {
                return input;
            }

            var chars = matchKanji.Length != 0 ? string.Join(string.Empty, matchKanji) : input;
            var tokenized = Tokenizer.Tokenize(chars);
            var okuriganaRegex = new Regex(
                isLeading ? $"^{tokenized[0].Content}" : $"{tokenized[tokenized.Length - 1].Content}$");
            return okuriganaRegex.Replace(input, string.Empty);
        }

        private static bool IsLeadingWithoutInitialKana(string input, bool isLeading) =>
            isLeading && !KanaChecker.IsKana(input[0]);

        private static bool IsTrailingWithoutFinalKana(string input, bool isLeading) =>
            !isLeading && !KanaChecker.IsKana(input[input.Length - 1]);

        private static bool IsInvalidMatcher(string input, params char[] matchKanji) =>
            (matchKanji.Length > 0 && !matchKanji.Any(KanjiChecker.IsKanji)) ||
            (matchKanji.Length == 0 && KanaChecker.IsKana(input));
    }
}
