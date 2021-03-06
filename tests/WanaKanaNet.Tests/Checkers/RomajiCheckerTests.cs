﻿using System;
using System.Text.RegularExpressions;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class RomajiCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.IsRomaji(null!));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(WanaKana.IsRomaji(string.Empty));
        }

        [InlineData("A", true)]
        [InlineData("xYz", true)]
        [InlineData("Tōkyō and Ōsaka", true)]
        [InlineData("あアA", false)]
        [InlineData("お願い", false)]
        [InlineData("熟成", false)]
        [InlineData("a*b&c-d", true)]
        [InlineData("0123456789", true)]
        [InlineData("a！b&cーd", false)]
        [InlineData("ｈｅｌｌｏ", false)]
        [Theory]
        public void IsRomajiResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, WanaKana.IsRomaji(input));
        }

        [Fact]
        public void IsRomajiWithOptionalAllowedChars()
        {
            var isRomaji = WanaKana.IsRomaji("a！b&cーd", '[', '！', 'ー', ']');
            Assert.True(isRomaji);
        }

        [Fact]
        public void IsRomajiWithOptionalAllowedCharsRegex()
        {
            var isRomaji = WanaKana.IsRomaji("a！b&cーd", new Regex("[！ー]"));
            Assert.True(isRomaji);
        }
    }
}