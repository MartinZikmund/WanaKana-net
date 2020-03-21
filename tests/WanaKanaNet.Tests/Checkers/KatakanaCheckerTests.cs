using System;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class KatakanaCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.IsKatakana(null!));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(WanaKana.IsKatakana(string.Empty));
        }

        [InlineData("アア", true)]
        [InlineData("ア", true)]
        [InlineData("あ", false)]
        [InlineData("A", false)]
        [InlineData("あア", false)]
        [InlineData("ゲーム", true)] //ignores long dash in katakana
        [Theory]
        public void IsKatakanaResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.IsKatakana(input));
        }
    }
}
