using System;
using System.Text.RegularExpressions;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class JapaneseCheckerTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => JapaneseChecker.IsJapanese(null));
        }

        [Fact]
        public void EmptyReturnsFalse()
        {
            Assert.False(JapaneseChecker.IsJapanese(string.Empty));
        }

        [InlineData("泣き虫", true)] //泣き虫 is Japanese
        [InlineData("あア", true)] //あア is Japanese
        [InlineData("A泣き虫", false)] //A泣き虫 is not Japanese
        [InlineData("A", false)] //A is not Japanese
        [InlineData("　", true)] //Japanese space
        [InlineData(" ", false)] //Normal space
        [InlineData("泣き虫。＃！〜〈〉《》〔〕［］【】（）｛｝〝〟", true)] //泣き虫。！〜 (w. zenkaku punctuation) is Japanese
        [InlineData("泣き虫.!~", false)] //泣き虫.!~ (w. romaji punctuation) is not Japanese
        [InlineData("０１２３４５６７８９", true)] //zenkaku numbers are considered neutral
        [InlineData("0123456789", false)] //latin numbers are not Japanese
        [InlineData("ＭｅＴｏｏ", true)] //zenkaku latin letters are considered neutral
        [InlineData("２０１１年", true)] //mixed with numbers is japanese
        [InlineData("ﾊﾝｶｸｶﾀｶﾅ", true)] //hankaku katakana is allowed
        [InlineData("＃ＭｅＴｏｏ、これを前に「ＫＵＲＯＳＨＩＯ」は、都内で報道陣を前に水中探査ロボットの最終点検の様子を公開しました。イルカのような形をした探査ロボットは、全長３メートル、重さは３５０キロあります。《はじめに》冒頭、安倍総理大臣は、ことしが明治元年から１５０年にあたることに触れ「明治という新しい時代が育てたあまたの人材が、技術優位の欧米諸国が迫る『国難』とも呼ぶべき危機の中で、わが国が急速に近代化を遂げる原動力となった。今また、日本は少子高齢化という『国難』とも呼ぶべき危機に直面している。もう１度、あらゆる日本人にチャンスを創ることで、少子高齢化も克服できる」と呼びかけました。《働き方改革》続いて安倍総理大臣は、具体的な政策課題の最初に「働き方改革」を取り上げ、「戦後の労働基準法制定以来、７０年ぶりの大改革だ。誰もが生きがいを感じて、その能力を思う存分発揮すれば少子高齢化も克服できる」と述べました。そして、同一労働同一賃金の実現や、時間外労働の上限規制の導入、それに労働時間でなく成果で評価するとして労働時間の規制から外す「高度プロフェッショナル制度」の創設などに取り組む考えを強調しました。", true)] //randomly sliced nhk news text is japanese
        [Theory]
        public void IsJapaneseResponsesMatch(string input, bool expectedResult)
        {
            Assert.Equal(expectedResult, JapaneseChecker.IsJapanese(input));
        }

        [Fact]
        public void IsJapaneseAcceptsOptionalAllowedChars()
        {
            var isJapanese = JapaneseChecker.IsJapanese("≪偽括弧≫", '[', '≪', '≫', ']');
            Assert.True(isJapanese);
        }

        [Fact]
        public void IsJapaneseAcceptsOptionalAllowedCharsRegex()
        {
            var isJapanese = JapaneseChecker.IsJapanese("≪偽括弧≫", new Regex("[≪≫]"));
            Assert.True(isJapanese);
        }
    }
}
