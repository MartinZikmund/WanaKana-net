using System;
using System.Linq;
using WanaKanaNet.Converters;
using WanaKanaNet.Tests.Helpers;
using Xunit;

namespace WanaKanaNet.Tests.Converters
{
    public class KanaConverterTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => KanaConverters.ToKana(null!));
        }

        [Fact]
        public void EmptyResultIsEmpty()
        {
            Assert.Empty(KanaConverters.ToKana(string.Empty));
        }

        [Fact]
        public void LowercaseCharactersAreTransliteratedToHiragana()
        {
            Assert.Equal("おなじ", KanaConverters.ToKana("onaji"));
        }

        [Fact]
        public void LowercaseWithDoubleConsonantsAndDoubleVowelsAreTransliteratedToHiragana()
        {
            Assert.Equal("ぶっつうじ", KanaConverters.ToKana("buttsuuji"));
        }

        [Fact]
        public void UppercaseCharactersAreTransliteratedToKatakana()
        {
            Assert.Equal("オナジ", KanaConverters.ToKana("ONAJI"));
        }

        [Fact]
        public void UppercaseWithDoubleConsonantsAndDoubleVowelsAreTransliteratedToKatakana()
        {
            Assert.Equal("ブッツウジ", KanaConverters.ToKana("BUTTSUUJI"));
        }

        [Fact]
        public void MixedCaseReturnsHiragana()
        {
            Assert.Equal("わにかに", KanaConverters.ToKana("WaniKani"));
        }

        [Fact]
        public void NonRomajiPassedThrough()
        {
            Assert.Equal("ワニカニ アいウえオ 鰐蟹 12345 @#$%", KanaConverters.ToKana("ワニカニ AiUeO 鰐蟹 12345 @#$%"));
        }

        [Fact]
        public void MixedSyllables()
        {
            Assert.Equal("座禅「ざぜん」スタイル", KanaConverters.ToKana("座禅‘zazen’スタイル"));
        }

        [Fact]
        public void ConvertShortToLongDashes()
        {
            Assert.Equal("ばつげーむ", KanaConverters.ToKana("batsuge-mu"));
        }

        [Fact]
        public void ConvertPunctuationButPassSpaces()
        {
            var input = string.Join(string.Empty, ConversionTables.EnglishPunctuation.Append(' '));
            var expected = string.Join(string.Empty, ConversionTables.JapanesePunctuation.Append(' '));
            Assert.Equal(expected, KanaConverters.ToKana(input));
        }

        [InlineData("n", "ん")]
        [InlineData("shin", "しん")]
        [InlineData("nn", "んん")]
        [Theory]
        public void WithoutImeMode(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverters.ToKana(input));
        }

        [InlineData("n", "n")]
        [InlineData("shin", "しn")]
        [InlineData("shinyou", "しにょう")]
        [InlineData("shin'you", "しんよう")]
        [InlineData("shin you", "しんよう")]
        [InlineData("nn", "ん")]
        [Theory]
        public void WithImeMode(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverters.ToKana(input, new WanaKanaOptions() { ImeMode = ImeMode.Enabled }));
        }

        [InlineData("wi", "うぃ")]
        [InlineData("WI", "ウィ")]
        [Theory]
        public void UseObsoleteKanaFalseByDefault(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverters.ToKana(input));
        }

        [InlineData("wi", "ゐ")]
        [InlineData("we", "ゑ")]
        [InlineData("WI", "ヰ")]
        [InlineData("WE", "ヱ")]
        [Theory]
        public void UsingObsoleteKana(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverters.ToKana(input, new WanaKanaOptions() { UseObsoleteKana = true }));
        }
    }
}
