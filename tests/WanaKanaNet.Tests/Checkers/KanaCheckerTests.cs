using System;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class KanaCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.IsKana(null!));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(WanaKana.IsKana(string.Empty));
        }

        [InlineData("あ", true)]
        [InlineData("ア", true)]
        [InlineData("あア", true)]
        [InlineData("A", false)]
        [InlineData("あAア", false)]
        [InlineData("アーあ", true)]
        [Theory]
        public void IsKanaResponseMatches(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.IsKana(input));
        }
    }
}
