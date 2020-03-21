using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Converters;
using WanaKanaNet.Enums;
using WanaKanaNet.Tests.Helpers;
using Xunit;

namespace WanaKanaNet.Tests.Converters
{
    public class KanaConverterTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => KanaConverter.ToKana(null!));
        }

        [Fact]
        public void EmptyResultIsEmpty()
        {
            Assert.Empty(KanaConverter.ToKana(string.Empty));
        }

        [Fact]
        public void LowercaseCharactersAreTransliteratedToHiragana()
        {
            Assert.Equal("おなじ", KanaConverter.ToKana("onaji"));
        }

        [Fact]
        public void LowercaseWithDoubleConsonantsAndDoubleVowelsAreTransliteratedToHiragana()
        {
            Assert.Equal("ぶっつうじ", KanaConverter.ToKana("buttsuuji"));
        }

        [Fact]
        public void UppercaseCharactersAreTransliteratedToKatakana()
        {
            Assert.Equal("オナジ", KanaConverter.ToKana("ONAJI"));
        }

        [Fact]
        public void UppercaseWithDoubleConsonantsAndDoubleVowelsAreTransliteratedToKatakana()
        {
            Assert.Equal("ブッツウジ", KanaConverter.ToKana("BUTTSUUJI"));
        }

        [Fact]
        public void MixedCaseReturnsHiragana()
        {
            Assert.Equal("わにかに", KanaConverter.ToKana("WaniKani"));
        }

        [Fact]
        public void NonRomajiPassedThrough()
        {
            Assert.Equal("ワニカニ アいウえオ 鰐蟹 12345 @#$%", KanaConverter.ToKana("ワニカニ AiUeO 鰐蟹 12345 @#$%"));
        }

        [Fact]
        public void MixedSyllables()
        {
            Assert.Equal("座禅「ざぜん」スタイル", KanaConverter.ToKana("座禅‘zazen’スタイル"));
        }

        [Fact]
        public void ConvertShortToLongDashes()
        {
            Assert.Equal("ばつげーむ", KanaConverter.ToKana("batsuge-mu"));
        }

        [Fact]
        public void ConvertPunctuationButPassSpaces()
        {
            var input = string.Join(string.Empty, ConversionTables.EnglishPunctuation.Append(' '));
            var expected = string.Join(string.Empty, ConversionTables.JapanesePunctuation.Append(' '));
            Assert.Equal(expected, KanaConverter.ToKana(input));
        }

        [InlineData("n", "ん")]
        [InlineData("shin", "しん")]
        [InlineData("nn", "んん")]
        [Theory]
        public void WithoutImeMode(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverter.ToKana(input));
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
            Assert.Equal(expectedResult, KanaConverter.ToKana(input, new WanaKanaOptions() { ImeMode = ImeMode.Enabled }));
        }

        [InlineData("wi", "うぃ")]
        [InlineData("WI", "ウィ")]
        [Theory]
        public void UseObsoleteKanaFalseByDefault(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverter.ToKana(input));
        }

        [InlineData("wi", "ゐ")]
        [InlineData("we", "ゑ")]
        [InlineData("WI", "ヰ")]
        [InlineData("WE", "ヱ")]
        [Theory]
        public void UsingObsoleteKana(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, KanaConverter.ToKana(input, new WanaKanaOptions() { UseObsoleteKana = true }));
        }
    }
}
