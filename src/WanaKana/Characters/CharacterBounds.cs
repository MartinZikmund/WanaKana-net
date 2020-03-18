using System.Linq;

namespace WanaKanaNet.Characters
{
    internal class CharacterBounds
    {
        public const int LatinLowercaseStart = 0x61;
        public const int LatinLowercaseEnd = 0x7a;
        public const int LatinUppercaseStart = 0x41;
        public const int LatinUppercaseEnd = 0x5a;
        public const int LowercaseZenkakuStart = 0xff41;
        public const int LowercaseZenkakuEnd = 0xff5a;
        public const int UppercaseZenkakuStart = 0xff21;
        public const int UppercaseZenkakuEnd = 0xff3a;
        public const int HiraganaStart = 0x3041;
        public const int HiraganaEnd = 0x3096;
        public const int KatakanaStart = 0x30a1;
        public const int KatakanaEnd = 0x30fc;
        public const int KanjiStart = 0x4e00;
        public const int KanjiEnd = 0x9faf;
        public const int ProlongedSoundMark = 0x30fc;
        public const int KanaSlashDot = 0x30fb;

        public static readonly UnicodeRange ZenkakuNumbers = new UnicodeRange(0xff10, 0xff19);
        public static readonly UnicodeRange ZenkakuUppercase = new UnicodeRange(UppercaseZenkakuStart, UppercaseZenkakuEnd);
        public static readonly UnicodeRange ZenkakuLowercase = new UnicodeRange(LowercaseZenkakuStart, LowercaseZenkakuEnd);
        public static readonly UnicodeRange ZenkakuPunctuation1 = new UnicodeRange(0xff01, 0xff0f);
        public static readonly UnicodeRange ZenkakuPunctuation2 = new UnicodeRange(0xff1a, 0xff1f);
        public static readonly UnicodeRange ZenkakuPunctuation3 = new UnicodeRange(0xff3b, 0xff3f);
        public static readonly UnicodeRange ZenkakuPunctuation4 = new UnicodeRange(0xff5b, 0xff60);
        public static readonly UnicodeRange ZenkakuSymbolsCurrency = new UnicodeRange(0xffe0, 0xffee);

        public static readonly UnicodeRange HiraganaChars = new UnicodeRange(0x3040, 0x309f);
        public static readonly UnicodeRange KatakanaChars = new UnicodeRange(0x30a0, 0x30ff);
        public static readonly UnicodeRange HankakuKatakana = new UnicodeRange(0xff66, 0xff9f);
        public static readonly UnicodeRange KatakanaPunctuation = new UnicodeRange(0x30fb, 0x30fc);
        public static readonly UnicodeRange KanaPunctuation = new UnicodeRange(0xff61, 0xff65);
        public static readonly UnicodeRange CjkSymbolsPunctuation = new UnicodeRange(0x3000, 0x303f);
        public static readonly UnicodeRange CommonCjk = new UnicodeRange(0x4e00, 0x9fff);
        public static readonly UnicodeRange RareCjk = new UnicodeRange(0x3400, 0x4dbf);

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

        public static readonly UnicodeRange ModernEnglish = new UnicodeRange(0x0000, 0x007f);
        public static readonly UnicodeRange[] HepburnMacronRanges = {
            new UnicodeRange(0x0100, 0x0101), // Ā ā
            new UnicodeRange(0x0112, 0x0113), // Ē ē
            new UnicodeRange(0x012a, 0x012b), // Ī ī
            new UnicodeRange(0x014c, 0x014d), // Ō ō
            new UnicodeRange(0x016a, 0x016b), // Ū ū
        };

        public static readonly UnicodeRange[] SmartQuoteRanges =
        {
            new UnicodeRange(0x2018, 0x2019), // ‘ ’

            new UnicodeRange(0x201c, 0x201d), // “ ” 
        };

        public static readonly UnicodeRange[] RomajiRanges = new[]
            {
                ModernEnglish
            }
            .Concat(HepburnMacronRanges)
            .ToArray();

        public static readonly UnicodeRange[] EnglishPunctuationRanges = new[]
            {
                new UnicodeRange(0x20, 0x2f),
                new UnicodeRange(0x3a, 0x3f),
                new UnicodeRange(0x5b, 0x60),
                new UnicodeRange(0x7b, 0x7e),
            }
            .Concat(SmartQuoteRanges)
            .ToArray();
    }
}
