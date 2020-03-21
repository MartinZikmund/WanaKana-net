using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Converters;
using Xunit;

namespace WanaKanaNet.Tests.Converters
{
    public class HiraganaConverterTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => HiraganaConverter.ToHiragana(null!));
        }

        [Fact]
        public void EmptyResultIsEmpty()
        {
            Assert.Empty(HiraganaConverter.ToHiragana(string.Empty));
        }

        // https://en.wikipedia.org/wiki/Iroha
        [InlineData("IROHANIHOHETO", "いろはにほへと")]
        [InlineData("CHIRINURUWO", "ちりぬるを")]
        [InlineData("WAKAYOTARESO", "わかよたれそ")]
        [InlineData("TSUNENARAMU", "つねならむ")]
        [InlineData("UWINOOKUYAMA", "うゐのおくやま")]
        [InlineData("KEFUKOETE", "けふこえて")]
        [InlineData("ASAKIYUMEMISHI", "あさきゆめみし")]
        [InlineData("WEHIMOSESU", "ゑひもせす")]
        [Theory]
        public void IrohaConversion(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaConverter.ToHiragana(input, new WanaKanaOptions() { UseObsoleteKana = true }), StringComparer.InvariantCulture);
        }

        [InlineData("NLTU", "んっ")]
        [Theory]
        public void ToHiraganaNltu(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaConverter.ToHiragana(input, new WanaKanaOptions() { UseObsoleteKana = true }), StringComparer.InvariantCulture);
        }

        [InlineData("wi", "うぃ")]
        [InlineData("NLTU", "んっ")]
        [Theory]
        public void ToHiraganaNotUsingObsoleteKanaExplicit(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaConverter.ToHiragana(input, new WanaKanaOptions() { UseObsoleteKana = false }), StringComparer.InvariantCulture);
        }

        [InlineData("wi", "ゐ")]
        [InlineData("we", "ゑ")]
        [Theory]
        public void ToHiraganaUsingObsoleteKana(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaConverter.ToHiragana(input, new WanaKanaOptions() { UseObsoleteKana = true }), StringComparer.InvariantCulture);
        }

        [Fact]
        public void ToHiraganaWithoutPassRomaji()
        {
            var result = HiraganaConverter.ToHiragana("only カナ");
            Assert.Equal("おんly かな", result, StringComparer.InvariantCulture);
        }

        [Fact]
        public void ToHiraganaWithPassRomaji()
        {
            var result = HiraganaConverter.ToHiragana("only カナ", new WanaKanaOptions() { PassRomaji = true });
            Assert.Equal("only かな", result, StringComparer.InvariantCulture);
        }

        [InlineData("スーパー", "すうぱあ")]
        [InlineData("バンゴー", "ばんごう")]
        [Theory]
        public void ToHiraganaKatakanaChoonpu(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaConverter.ToHiragana(input), StringComparer.InvariantCulture);
        }

        [Fact]
        public void ToHiraganaWithMixedInput()
        {
            var result = HiraganaConverter.ToHiragana("#22 ２２漢字、toukyou, オオサカ");
            Assert.Equal("#22 ２２漢字、とうきょう、 おおさか", result, StringComparer.InvariantCulture);
        }


        [Fact]
        public void HiraganaToKatakanaNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => HiraganaConverter.HiraganaToKatakana(null));
        }

        [InlineData("あはべか", "アハベカ")]
        [InlineData("ぱぺぷれ", "パペプレ")]
        [Theory]
        public void HiraganaToKatakanaSamples(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaConverter.HiraganaToKatakana(input));
        }
    }
}
