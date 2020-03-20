using System.Text;
using WanaKanaNet.Characters;
using WanaKanaNet.Checkers;

namespace WanaKanaNet.Converters
{
    internal static class HiraganaConverter
    {
        public static string ToHiragana(string input, WanaKanaOptions? options = null)
        {
            options ??= new WanaKanaOptions();

            if (options.PassRomaji)
            {
                return KatakanaConverter.KatakanaToHiragana(input);
            }

            if (MixedChecker.IsMixed(input))
            {
                var convertedKatakana = KatakanaConverter.KatakanaToHiragana(input);
                return KanaConverter.ToKana(convertedKatakana.ToLowerInvariant(), options);
            }

            if (RomajiChecker.IsRomaji(input) || SpecialCharacterChecker.IsEnglishPunctuation(input[0]))
            {
                return KanaConverter.ToKana(input.ToLowerInvariant(), options);
            }

            return KatakanaConverter.KatakanaToHiragana(input);
        }

        public static string HiraganaToKatakana(string input)
        {
            if (input is null)
            {
                throw new System.ArgumentNullException(nameof(input));
            }

            var builder = new StringBuilder();
            foreach (var character in input)
            {
                // Short circuit to avoid incorrect codeshift for 'ー' and '・'
                if (SpecialCharacterChecker.IsLongDash(character) || SpecialCharacterChecker.IsSlashDot(character))
                {
                    builder.Append(character);
                }
                else if (HiraganaChecker.IsHiragana(character))
                {
                    // Shift charcode.
                    var katakanaCode = (character - CharacterBounds.HiraganaStart) + CharacterBounds.KatakanaStart;
                    var katakanaCharacter = (char)katakanaCode;
                    builder.Append(katakanaCharacter);
                }
                else
                {
                    // Pass non-hiragana chars through
                    builder.Append(character);
                }
            }
            return builder.ToString();
        }
    }
}
