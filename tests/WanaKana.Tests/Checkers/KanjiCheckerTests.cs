using System;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class KanjiCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => KanjiChecker.IsKanji(null));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(KanjiChecker.IsKanji(string.Empty));
        }

        [InlineData("切腹", true)]
        [InlineData("刀", true)]
        [InlineData("🐸", false)]
        [InlineData("あ", false)]
        [InlineData("ア", false)]
        [InlineData("あア", false)]
        [InlineData("A", false)]
        [InlineData("あAア", false)]
        [InlineData("１２隻", false)]
        [InlineData("12隻", false)]
        [InlineData("隻。", false)]
        [Theory]
        public void IsKanjiResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, KanjiChecker.IsKanji(input));
        }
    }
}
