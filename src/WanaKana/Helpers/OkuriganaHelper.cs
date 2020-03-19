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
        public string StripOkurigana(string input, bool isLeading = false, string matchKanji = "")
        {
            if (
                !JapaneseChecker.IsJapanese(input) ||
                IsLeadingWithoutInitialKana(input, isLeading) ||
                IsTrailingWithoutFinalKana(input, isLeading) ||
                IsInvalidMatcher(input, matchKanji))
            {
                return input;
            }

            var chars = matchKanji != null ? matchKanji : input;
            var okuriganaRegex = new Regex(
                isLeading ? $"^{Tokenizer.Tokenize(chars)}" : $"{Tokenizer.Tokenize(chars).Pop()}$";
        }

        private static bool IsLeadingWithoutInitialKana(string input, bool isLeading) =>
            isLeading && !KanaChecker.IsKana(input[0]);

        private static bool IsTrailingWithoutFinalKana(string input, bool isLeading) =>
            !isLeading && !KanaChecker.IsKana(input[input.Length - 1]);

        private static bool IsInvalidMatcher(string input, string matchKanji) =>
            (matchKanji?.Length > 0 && matchKanji.Any(KanjiChecker.IsKanji)) ||
            ((matchKanji?.Length ?? 0) == 0 && KanaChecker.IsKana(input));
    }
}
