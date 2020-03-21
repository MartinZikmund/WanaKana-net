using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using WanaKanaNet.Converters;
using WanaKanaNet.Tests.Helpers;
using Xunit;

namespace WanaKanaNet.Tests
{
    public class ConversionsTests
    {
        [Fact]
        public void ToKanaTableConversions()
        {
            foreach (var (romaji, hiragana, katakana) in ConversionTables.RomajiToHiraganaKatakana)
            {
                var lower = KanaConverter.ToKana(romaji);
                var upper = KanaConverter.ToKana(romaji.ToUpperInvariant());
                Assert.Equal(hiragana, lower);
                Assert.Equal(katakana, upper);
            }
        }

        [Fact]
        public void ToHiraganaTableConversions()
        {
            foreach (var (romaji, hiragana, katakana) in ConversionTables.RomajiToHiraganaKatakana)
            {
                var lower = HiraganaConverter.ToHiragana(romaji);
                var upper = HiraganaConverter.ToHiragana(romaji.ToUpperInvariant());
                Assert.Equal(hiragana, lower);
                Assert.Equal(hiragana, upper);
            }
        }

        [Fact]
        public void ToKatakanaTableConversions()
        {
            foreach (var (romaji, hiragana, katakana) in ConversionTables.RomajiToHiraganaKatakana)
            {
                var lower = KatakanaConverter.ToKatakana(romaji);
                var upper = KatakanaConverter.ToKatakana(romaji.ToUpperInvariant());
                Assert.Equal(katakana, lower);
                Assert.Equal(katakana, upper);
            }
        }

        [Fact]
        public void HiraganaToRomaji()
        {
            foreach (var (hiragana, _, romaji) in ConversionTables.HiraganaKatakanaToRomaji)
            {
                if (!string.IsNullOrEmpty(hiragana))
                {
                    var result = RomajiConverter.ToRomaji(hiragana);
                    Assert.Equal(romaji, result);
                }
            }
        }

        [Fact]
        public void KatakanaToRomaji()
        {
            foreach (var (_, katakana, romaji) in ConversionTables.HiraganaKatakanaToRomaji)
            {
                if (!string.IsNullOrEmpty(katakana))
                {
                    var result = RomajiConverter.ToRomaji(katakana);
                    Assert.Equal(romaji, result);
                }
            }
        }

        [InlineData("バケル", "ばける")]
        [InlineData("すたーいる", "すたーいる")]
        [InlineData("アメリカじん", "あめりかじん")]
        [Theory]
        public void ConvertingKatakanaToHiragana(string katakana, string hiragana)
        {
            var result = HiraganaConverter.ToHiragana(katakana);
            Assert.Equal(hiragana, result);
        }

        [InlineData("ばける", "バケル")]
        [InlineData("スタイル", "スタイル")]
        [InlineData("アメリカじん", "アメリカジン")]
        [Theory]
        public void ConvertingHiraganaToKatakana(string hiragana, string katakana)
        {
            var result = KatakanaConverter.ToKatakana(hiragana);
            Assert.Equal(katakana, result);
        }

        [InlineData("バツゴー", "ばつごう")]
        [InlineData("ばつげーむ", "ばつげーむ")]
        [InlineData("てすート", "てすーと")]
        [InlineData("てすー戸", "てすー戸")]
        [InlineData("手巣ート", "手巣ーと")]
        [InlineData("tesート", "てsーと")]
        [InlineData("ートtesu", "ーとてす")]
        [Theory]
        public void ConvertingLongVowelsToHiragana(string input, string expectedResult)
        {
            var result = HiraganaConverter.ToHiragana(input);
            Assert.Equal(expectedResult, result);
        }

        [InlineData("ばつゲーム", "バツゲーム")]
        [InlineData("バツゲーム", "バツゲーム")]
        [InlineData("テスーと", "テスート")]
        [Theory]
        public void ConvertingLongVowelsToKatakana(string input, string expectedResult)
        {
            var result = KatakanaConverter.ToKatakana(input);
            Assert.Equal(expectedResult, result);
        }

        [InlineData("座禅‘zazen’スタイル", true, "座禅‘zazen’すたいる")]
        [InlineData("座禅‘zazen’スタイル", false, "座禅「ざぜん」すたいる")]
        [Theory]
        public void MixedSyllablesToHiragana(string input, bool passRomaji, string expectedResult)
        {
            var result = HiraganaConverter.ToHiragana(input, new WanaKanaOptions() { PassRomaji = passRomaji });
            Assert.Equal(expectedResult, result);
        }

        [InlineData("座禅‘zazen’すたいる", true, "座禅‘zazen’スタイル")]
        [InlineData("座禅‘zazen’すたいる", false, "座禅「ザゼン」スタイル")]
        [Theory]
        public void MixedSyllablesToKatakana(string input, bool passRomaji, string expectedResult)
        {
            var result = KatakanaConverter.ToKatakana(input, new WanaKanaOptions() { PassRomaji = passRomaji });
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CaseSensitivityToHiragana()
        {
            var result = HiraganaConverter.ToHiragana("aiueo");
            var upperResult = HiraganaConverter.ToHiragana("AIUEO");
            Assert.Equal(upperResult, result);
        }

        [Fact]
        public void CaseSensitivityToKatakana()
        {
            var result = KatakanaConverter.ToKatakana("aiueo");
            var upperResult = KatakanaConverter.ToKatakana("AIUEO");
            Assert.Equal(upperResult, result);
        }

        [Fact]
        public void CaseSensitivityToKana()
        {
            var result = KanaConverter.ToKana("aiueo");
            var upperResult = KanaConverter.ToKana("AIUEO");
            Assert.NotEqual(upperResult, result);
        }

        [InlineData("n", "ん")]
        [InlineData("onn", "おんん")]
        [InlineData("onna", "おんな")]
        [InlineData("nnn", "んんん")]
        [InlineData("onnna", "おんんな")]
        [InlineData("nnnn", "んんんん")]
        [InlineData("nyan", "にゃん")]
        [InlineData("nnyann", "んにゃんん")]
        [InlineData("nnnyannn", "んんにゃんんん")]
        [InlineData("n'ya", "んや")]
        [InlineData("kin'ya", "きんや")]
        [InlineData("shin'ya", "しんや")]
        [InlineData("kinyou", "きにょう")]
        [InlineData("kin'you", "きんよう")]
        [InlineData("kin'yu", "きんゆ")]
        [InlineData("ichiban warui", "いちばん わるい")]
        [InlineData("chya", "ちゃ")]
        [InlineData("chyx", "chyx")]
        [InlineData("shyp", "shyp")]
        [InlineData("ltsb", "ltsb")]
        [Theory]
        public void KanaEdgeCases(string input, string expectedResult)
        {
            var result = KanaConverter.ToKana(input);
            Assert.Equal(expectedResult, result);
        }
    }
}
