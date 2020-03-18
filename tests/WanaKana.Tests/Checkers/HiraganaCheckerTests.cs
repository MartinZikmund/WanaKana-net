using System;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class HiraganaCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => HiraganaChecker.IsHiragana(null));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(HiraganaChecker.IsHiragana(string.Empty));
        }

        [InlineData("あ", true)]
        [InlineData("ああ", true)]
        [InlineData("ア", true)]
        [InlineData("A", true)]
        [InlineData("あア", false)]
        [InlineData("げーむ", true)] //ignores long dash in hiragana
        [Theory]
        public void IsHiraganaResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, HiraganaChecker.IsHiragana(input));
        }
    }
}
