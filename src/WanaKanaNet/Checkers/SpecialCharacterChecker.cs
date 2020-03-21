using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    internal static class SpecialCharacterChecker
    {
        public static bool IsLongDash(char character) =>
            character == CharacterRanges.ProlongedSoundMark;

        public static bool IsSlashDot(char character) =>
            character == CharacterRanges.KanaSlashDot;

        public static bool IsConsonant(char character, bool includeY = true)
        {
            var lowerCase = char.ToLowerInvariant(character);
            return CharacterRanges.Consonants.Contains(lowerCase) || (includeY && lowerCase == 'y');
        }

        public static bool IsVowel(char character, bool includeY = true)
        {
            var lowerCase = char.ToLowerInvariant(character);
            return CharacterRanges.Vowels.Contains(lowerCase) || (includeY && lowerCase == 'y');
        }

        public static bool IsPunctuation(char character) =>
            IsEnglishPunctuation(character) || IsJapanesePunctuation(character);

        public static bool IsEnglishPunctuation(char character) =>
            CharacterRanges.EnglishPunctuationRanges.Any(range => range.Contains(character));

        public static bool IsJapanesePunctuation(char character) =>
            CharacterRanges.JapanesePunctuationRanges.Any(range => range.Contains(character));
    }
}
