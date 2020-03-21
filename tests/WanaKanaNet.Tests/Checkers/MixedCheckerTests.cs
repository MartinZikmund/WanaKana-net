using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class MixedCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.IsMixed(null!));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(WanaKana.IsMixed(string.Empty));
        }

        [InlineData("Aア", true)]
        [InlineData("Aあ", true)]
        [InlineData("Aあア", true)]
        [InlineData("２あア", false)]
        [InlineData("お腹A", true)]
        [InlineData("お腹", false)]
        [InlineData("腹", false)]
        [InlineData("A", false)]
        [InlineData("あ", false)]
        [InlineData("ア", false)]
        [Theory]
        public void IsMixedResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.IsMixed(input));
        }

        [Fact]
        public void IsMixedWithPassKanji()
        {
            var isMixed = WanaKana.IsMixed("お腹A", passKanji: false);
            Assert.False(isMixed);
        }
    } 
}
