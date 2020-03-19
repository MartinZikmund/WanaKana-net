using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class SpecialCharacterCheckers
    {
        public static bool IsLongDash(char character) =>
            character == CharacterBounds.ProlongedSoundMark;

        public static bool IsSlashDot(char character) =>
            character == CharacterBounds.KanaSlashDot;

        public static bool IsConsonant(char character, bool includeY = true)
        {
            var lowerCase = char.ToLowerInvariant(character);
            return CharacterBounds.Consonants.Contains(lowerCase) || (includeY && lowerCase == 'y');
        }

        public static bool IsVowel(char character, bool includeY = true)
        {
            var lowerCase = char.ToLowerInvariant(character);
            return CharacterBounds.Vowels.Contains(lowerCase) || (includeY && lowerCase == 'y');
        }

        public static bool IsPunctuation(char character) =>
            IsEnglishPunctuation(character) || IsJapanesePunctuation(character);

        public static bool IsEnglishPunctuation(char character) =>
            CharacterBounds.EnglishPunctuationRanges.Any(range => range.Contains(character));

        public static bool IsJapanesePunctuation(char character) =>
            CharacterBounds.JapanesePunctuationRanges.Any(range => range.Contains(character));
    }
}
