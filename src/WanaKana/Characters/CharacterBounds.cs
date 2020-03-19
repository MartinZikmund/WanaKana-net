using System.Collections.Generic;
using System.Linq;

namespace WanaKanaNet.Characters
{
    internal class CharacterBounds
    {
        public const char LatinLowercaseStart = (char)0x61;
        public const char LatinLowercaseEnd = (char)0x7a;
        public const char LatinUppercaseStart = (char)0x41;
        public const char LatinUppercaseEnd = (char)0x5a;
        public const char LowercaseZenkakuStart = (char)0xff41;
        public const char LowercaseZenkakuEnd = (char)0xff5a;
        public const char UppercaseZenkakuStart = (char)0xff21;
        public const char UppercaseZenkakuEnd = (char)0xff3a;
        public const char HiraganaStart = (char)0x3041;
        public const char HiraganaEnd = (char)0x3096;
        public const char KatakanaStart = (char)0x30a1;
        public const char KatakanaEnd = (char)0x30fc;
        public const char KanjiStart = (char)0x4e00;
        public const char KanjiEnd = (char)0x9faf;
        public const char ProlongedSoundMark = (char)0x30fc;
        public const char KanaSlashDot = (char)0x30fb;

        public static readonly UnicodeRange KanjiRange = new UnicodeRange(KanjiStart, KanjiEnd);

        public static readonly UnicodeRange ZenkakuNumbers = new UnicodeRange((char)0xff10, (char)0xff19);
        public static readonly UnicodeRange ZenkakuUppercase = new UnicodeRange(UppercaseZenkakuStart, UppercaseZenkakuEnd);
        public static readonly UnicodeRange ZenkakuLowercase = new UnicodeRange(LowercaseZenkakuStart, LowercaseZenkakuEnd);
        public static readonly UnicodeRange ZenkakuPunctuation1 = new UnicodeRange((char)0xff01, (char)0xff0f);
        public static readonly UnicodeRange ZenkakuPunctuation2 = new UnicodeRange((char)0xff1a, (char)0xff1f);
        public static readonly UnicodeRange ZenkakuPunctuation3 = new UnicodeRange((char)0xff3b, (char)0xff3f);
        public static readonly UnicodeRange ZenkakuPunctuation4 = new UnicodeRange((char)0xff5b, (char)0xff60);
        public static readonly UnicodeRange ZenkakuSymbolsCurrency = new UnicodeRange((char)0xffe0, (char)0xffee);

        public static readonly UnicodeRange HiraganaChars = new UnicodeRange((char)0x3040, (char)0x309f);
        public static readonly UnicodeRange KatakanaChars = new UnicodeRange((char)0x30a0, (char)0x30ff);
        public static readonly UnicodeRange HankakuKatakana = new UnicodeRange((char)0xff66, (char)0xff9f);
        public static readonly UnicodeRange KatakanaPunctuation = new UnicodeRange((char)0x30fb, (char)0x30fc);
        public static readonly UnicodeRange KanaPunctuation = new UnicodeRange((char)0xff61, (char)0xff65);
        public static readonly UnicodeRange CjkSymbolsPunctuation = new UnicodeRange((char)0x3000, (char)0x303f);
        public static readonly UnicodeRange CommonCjk = new UnicodeRange((char)0x4e00, (char)0x9fff);
        public static readonly UnicodeRange RareCjk = new UnicodeRange((char)0x3400, (char)0x4dbf);

        /// <summary>
        /// Consonants, does not include 'y'.
        /// </summary>
        public static readonly HashSet<char> Consonants = new HashSet<char>()
        {
            'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'm', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z',
        };

        /// <summary>
        /// Vowels, does not include 'y'.
        /// </summary>
        public static readonly HashSet<char> Vowels = new HashSet<char>()
        {
            'a', 'e', 'i', 'o', 'u',
        };

        public static readonly UnicodeRange[] KanaRanges = {
            HiraganaChars, KatakanaChars, KanaPunctuation, HankakuKatakana
        };

        public static readonly UnicodeRange[] JapanesePunctuationRanges = {
            CjkSymbolsPunctuation,
            KanaPunctuation,
            KatakanaPunctuation,
            ZenkakuPunctuation1,
            ZenkakuPunctuation2,
            ZenkakuPunctuation3,
            ZenkakuPunctuation4,
            ZenkakuSymbolsCurrency
        };

        public static readonly UnicodeRange[] JapaneseRanges =
            KanaRanges
                .Concat(JapanesePunctuationRanges)
                .Concat(new[]
                {
                    ZenkakuUppercase,
                    ZenkakuLowercase,
                    ZenkakuNumbers,
                    CommonCjk,
                    RareCjk,
                })
                .ToArray();

        public static readonly UnicodeRange ModernEnglish = new UnicodeRange((char)0x0000, (char)0x007f);
        public static readonly UnicodeRange[] HepburnMacronRanges = {
            new UnicodeRange((char)0x0100, (char)0x0101), // Ā ā
            new UnicodeRange((char)0x0112, (char)0x0113), // Ē ē
            new UnicodeRange((char)0x012a, (char)0x012b), // Ī ī
            new UnicodeRange((char)0x014c, (char)0x014d), // Ō ō
            new UnicodeRange((char)0x016a, (char)0x016b), // Ū ū
        };

        public static readonly UnicodeRange[] SmartQuoteRanges =
        {
            new UnicodeRange((char)0x2018, (char)0x2019), // ‘ ’

            new UnicodeRange((char)0x201c, (char)0x201d), // “ ” 
        };

        public static readonly UnicodeRange[] RomajiRanges = new[]
            {
                ModernEnglish
            }
            .Concat(HepburnMacronRanges)
            .ToArray();

        public static readonly UnicodeRange[] EnglishPunctuationRanges = new[]
            {
                new UnicodeRange((char)0x20, (char)0x2f),
                new UnicodeRange((char)0x3a, (char)0x3f),
                new UnicodeRange((char)0x5b, (char)0x60),
                new UnicodeRange((char)0x7b, (char)0x7e),
            }
            .Concat(SmartQuoteRanges)
            .ToArray();
    }
}
