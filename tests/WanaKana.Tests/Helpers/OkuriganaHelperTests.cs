using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Helpers;
using Xunit;

namespace WanaKanaNet.Tests.Helpers
{
    public class OkuriganaHelperTests
    {
        [Fact]
        public void EmptyInput()
        {
            Assert.Equal(string.Empty, OkuriganaHelpers.StripOkurigana(string.Empty));
        }

        [InlineData("ふふフフ", "ふふフフ")]
        [InlineData("abc", "abc")]
        [InlineData("ふaふbフcフ", "ふaふbフcフ")]
        [Theory]
        public void BasicInputs(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, OkuriganaHelpers.StripOkurigana(input));
        }

        [InlineData("踏み込む", "踏み込")]
        [InlineData("使い方", "使い方")]
        [InlineData("申し申し", "申し申")]
        [InlineData("お腹", "お腹")]
        [InlineData("お祝い", "お祝")]
        [Theory]
        public void DefaultParameters(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, OkuriganaHelpers.StripOkurigana(input));
        }

        [InlineData("踏み込む", "踏み込む")]
        [InlineData("お腹", "腹")]
        [InlineData("お祝い", "祝い")]
        [Theory]
        public void StripLeading(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, OkuriganaHelpers.StripOkurigana(input, isLeading: true));
        }

        [InlineData("おはら", false, "お,腹", "おはら")]
        [InlineData("ふみこむ", false, "踏,み,込,む", "ふみこ")]
        [InlineData("おみまい", true, "お,祝,い", "みまい")]
        [InlineData("おはら", true, "お,腹", "はら")]
        [Theory]
        public void StripWithMatchKanji(string input, bool isLeading, string matchKanji, string expectedResult)
        {
            var kanji = matchKanji.Split(',').Select(s => s[0]);
            Assert.Equal(expectedResult, OkuriganaHelpers.StripOkurigana(input, isLeading, kanji.ToArray()));
        }
    }
}
