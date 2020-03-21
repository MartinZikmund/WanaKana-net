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
                var result = RomajiConverter.ToRomaji(hiragana);
                Assert.Equal(romaji, result);
            }
        }

        [Fact]
        public void KatakanaToRomaji()
        {
            foreach (var (_, katakana, romaji) in ConversionTables.HiraganaKatakanaToRomaji)
            {
                var result = RomajiConverter.ToRomaji(katakana);
                Assert.Equal(romaji, result);
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
    }
}
