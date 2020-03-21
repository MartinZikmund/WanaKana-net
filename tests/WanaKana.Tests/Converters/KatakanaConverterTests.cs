using System;
using System.Collections.Generic;
using System.Text;
using WanaKanaNet.Converters;
using Xunit;

namespace WanaKanaNet.Tests.Converters
{
    public class KatakanaConverterTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.ToKatakana(null!));
        }

        [Fact]
        public void EmptyResultIsEmpty()
        {
            Assert.Empty(WanaKana.ToKatakana(string.Empty));
        }

        // https://en.wikipedia.org/wiki/Iroha
        [InlineData("IROHANIHOHETO", "イロハニホヘト")]
        [InlineData("CHIRINURUWO", "チリヌルヲ")]
        [InlineData("WAKAYOTARESO", "ワカヨタレソ")]
        [InlineData("TSUNENARAMU", "ツネナラム")]
        [InlineData("UWINOOKUYAMA", "ウヰノオクヤマ")]
        [InlineData("KEFUKOETE", "ケフコエテ")]
        [InlineData("ASAKIYUMEMISHI", "アサキユメミシ")]
        [InlineData("WEHIMOSESU", "ヱヒモセス")]
        [Theory]
        public void IrohaConversion(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.ToKatakana(input, new WanaKanaOptions() { UseObsoleteKana = true }), StringComparer.InvariantCulture);
        }

        [InlineData("NLTU", "ンッ")]
        [Theory]
        public void ToKatakanaNltu(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.ToKatakana(input, new WanaKanaOptions() { UseObsoleteKana = true }), StringComparer.InvariantCulture);
        }

        [InlineData("wi", "ウィ")]
        [InlineData("NLTU", "ンッ")]
        [Theory]
        public void ToHiraganaNotUsingObsoleteKanaExplicit(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.ToKatakana(input, new WanaKanaOptions() { UseObsoleteKana = false }), StringComparer.InvariantCulture);
        }

        [InlineData("wi", "ヰ")]
        [InlineData("we", "ヱ")]
        [Theory]
        public void ToHiraganaUsingObsoleteKana(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.ToKatakana(input, new WanaKanaOptions() { UseObsoleteKana = true }), StringComparer.InvariantCulture);
        }

        [Fact]
        public void ToHiraganaWithoutPassRomaji()
        {
            var result = WanaKana.ToKatakana("only かな");
            Assert.Equal("オンly カナ", result, StringComparer.InvariantCulture);
        }

        [Fact]
        public void ToHiraganaWithPassRomaji()
        {
            var result = WanaKana.ToKatakana("only かな", new WanaKanaOptions() { PassRomaji = true });
            Assert.Equal("only カナ", result, StringComparer.InvariantCulture);
        }
    }
}
