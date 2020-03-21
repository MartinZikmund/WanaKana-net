using System;
using System.Collections.Generic;
using System.Linq;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Helpers
{
    internal static class Tokenizer
    {
        public static Token[] Tokenize(string input, bool compact = false)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input == string.Empty)
            {
                return Array.Empty<Token>();
            }

            var chars = input.ToCharArray();
            var initialCharacter = chars[0];
            var previousType = GetTokenType(initialCharacter, compact);
            var initial = new Token(initialCharacter, previousType);

            var result = new List<Token>();
            result.Add(initial);

            foreach (var character in chars.Skip(1))
            {
                var currentType = GetTokenType(character, compact);
                var sameType = currentType == previousType;
                previousType = currentType;

                var newValue = character;
                if (sameType)
                {
                    var extendedToken = result[result.Count - 1].Extend(character);
                    result[result.Count - 1] = extendedToken;
                }
                else
                {
                    result.Add(new Token(newValue, currentType));
                }
            }

            return result.ToArray();
        }

        private static string GetTokenType(char character, bool compact = false)
        {
            if (compact)
            {
                if (IsJaNumber(character)) return TokenTypes.Other;
                if (IsEnNumber(character)) return TokenTypes.Other;
                if (IsEnSpace(character)) return TokenTypes.English;
                if (SpecialCharacterChecker.IsEnglishPunctuation(character)) return TokenTypes.Other;
                if (IsJaSpace(character)) return TokenTypes.Japanese;
                if (SpecialCharacterChecker.IsJapanesePunctuation(character)) return TokenTypes.Other;
                if (JapaneseChecker.IsJapanese(character)) return TokenTypes.Japanese;
                if (RomajiChecker.IsRomaji(character)) return TokenTypes.English;
            }
            else
            {
                if (IsJaSpace(character)) return TokenTypes.Space;
                if (IsEnSpace(character)) return TokenTypes.Space;
                if (IsJaNumber(character)) return TokenTypes.JapaneseNumber;
                if (IsEnNumber(character)) return TokenTypes.EnglishNumber;
                if (SpecialCharacterChecker.IsEnglishPunctuation(character)) return TokenTypes.EnglishPunctuation;
                if (SpecialCharacterChecker.IsJapanesePunctuation(character)) return TokenTypes.JapanesePunctuation;
                if (KanjiChecker.IsKanji(character)) return TokenTypes.Kanji;
                if (HiraganaChecker.IsHiragana(character)) return TokenTypes.Hiragana;
                if (KatakanaChecker.IsKatakana(character)) return TokenTypes.Katakana;
                if (JapaneseChecker.IsJapanese(character)) return TokenTypes.Japanese;
                if (RomajiChecker.IsRomaji(character)) return TokenTypes.English;
            }
            return TokenTypes.Other;
        }

        private static bool IsEnSpace(char character) => character == ' ';

        private static bool IsJaSpace(char character) => character == '　';

        private static bool IsEnNumber(char character) => '0' <= character && character <= '9';

        private static bool IsJaNumber(char character) => '０' <= character && character <= '９';
    }
}
