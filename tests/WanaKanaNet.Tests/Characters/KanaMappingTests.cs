using System;
using WanaKanaNet.Converters;
using Xunit;

namespace WanaKanaNet.Tests.Characters
{
    public class KanaMappingTests
    {
        [Fact]
        public void CustomMappingApplied()
        {
            var mapping = KanaConverters.ToKana("wanakana", new WanaKanaOptions() { CustomKanaMapping = { { "na", "に" }, { "ka", "Bana" } } });
            Assert.Equal("わにBanaに", mapping);
        }

        [Fact]
        public void CantRomanizeWithInvalidMethod()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => WanaKana.ToRomaji("つじぎり", new WanaKanaOptions() { Romanization = (RomanizationType)6 }));
        }

        [Fact]
        public void CustomRomajiMapping()
        {
            var result = WanaKana.ToRomaji("つじぎり", new WanaKanaOptions() { CustomRomajiMapping = { { "じ", "zi" }, { "つ", "tu" }, { "り", "li" } } });
            Assert.Equal("tuzigili", result);
        }
    }
}
