using System;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class HiraganaCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.IsHiragana(null!));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(WanaKana.IsHiragana(string.Empty));
        }

        [InlineData("あ", true)]
        [InlineData("ああ", true)]
        [InlineData("ア", false)]
        [InlineData("A", false)]
        [InlineData("あア", false)]
        [InlineData("げーむ", true)] //ignores long dash in hiragana
        [Theory]
        public void IsHiraganaResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.IsHiragana(input));
        }
    }
}
