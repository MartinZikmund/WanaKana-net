using System;
using WanaKanaNet.Converters;
using WanaKanaNet.Enums;
using Xunit;

namespace WanaKanaNet.Tests.Characters
{
    public class KanaMappingTests
    {
        [Fact]
        public void CustomMappingApplied()
        {
            var mapping = KanaConverter.ToKana("wanakana", new WanaKanaOptions() { CustomKanaMapping = { { "na", "に" }, { "ka", "Bana" } } });
            Assert.Equal("わにBanaに", mapping);
        }

        [Fact]
        public void CantRomanizeWithInvalidMethod()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => RomajiConverter.ToRomaji("つじぎり", new WanaKanaOptions() { Romanization = (RomanizationMap)6 }));
        }

        [Fact]
        public void CustomRomajiMapping()
        {
            var result = RomajiConverter.ToRomaji("つじぎり", new WanaKanaOptions() { CustomRomajiMapping = { { "じ", "zi" }, { "つ", "tu" }, { "り", "li" } } });
            Assert.Equal("tuzigili", result);
        }
    }
}
