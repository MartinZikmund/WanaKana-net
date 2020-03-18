using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class KanaCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => KanaChecker.IsKana(null));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(KanaChecker.IsKana(string.Empty));
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
            Assert.Equal(expectedResult, KanaChecker.IsKana(input));
        }
    }
}
