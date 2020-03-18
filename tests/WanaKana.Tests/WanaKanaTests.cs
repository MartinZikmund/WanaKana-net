using WanaKanaNet;
using Xunit;

namespace WanaKanaNet.Tests
{
    public class WanaKanaTests
    {
        [Fact]
        public void NullArgumentThrows()
        {
            WanaKana.ToHiragana(null);
        }
    }
}
