using System;
using Xunit;

namespace WanaKanaNet.Tests
{
    public class ToHiraganaTests
    {
        [Fact]
        public void NullArgumentThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.ToHiragana(null));
        }
    }
}
