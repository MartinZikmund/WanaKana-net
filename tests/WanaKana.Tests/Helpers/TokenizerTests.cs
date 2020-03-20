using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Helpers;
using Xunit;

namespace WanaKanaNet.Tests.Helpers
{
    public class TokenizerTests
    {
        [Fact]
        public void EmptyInput()
        {
            Assert.Empty(Tokenizer.Tokenize(string.Empty));
        }

        [Fact]
        public void NullInput()
        {
            Assert.Empty(Tokenizer.Tokenize(null));
        }

        [InlineData("ふふ", "ふふ")]
        [InlineData("フフ", "フフ")]
        [InlineData("ふふフフ", "ふふ,フフ")]
        [InlineData("阮咸", "阮咸")]
        [InlineData("感じ", "感,じ")]
        [InlineData("私は悲しい", "私,は,悲,しい")]
        [InlineData("ok لنذهب!", "ok, ,لنذهب,!")]
        [Theory]
        public void BasicTests(string input, string expectedResult)
        {
            var result = Tokenizer.Tokenize(input);
            var split = expectedResult.Split(',');
            Assert.Equal(split, result.Select(t => t.Content));
        }

        [Fact]
        public void HandlesMixedInput()
        {
            var result = Tokenizer.Tokenize("5romaji here...!?漢字ひらがなカタ　カナ４「ＳＨＩＯ」。！");
            var expectedResult = new string[]
            {
                "5",
                "romaji",
                " ",
                "here",
                "...!?",
                "漢字",
                "ひらがな",
                "カタ",
                "　",
                "カナ",
                "４",
                "「",
                "ＳＨＩＯ",
                "」。！",
            };
            Assert.Equal(expectedResult, result.Select(t => t.Content));
        }

        [Fact]
        public void CompactTrue()
        {
            var result = Tokenizer.Tokenize("5romaji here...!?漢字ひらがなカタ　カナ４「ＳＨＩＯ」。！ لنذهب", true);
            var expectedResult = new string[]
            {
                "5",
                "romaji here",
                "...!?",
                "漢字ひらがなカタ　カナ",
                "４「",
                "ＳＨＩＯ",
                "」。！",
                " ",
                "لنذهب",
            };
            Assert.Equal(expectedResult, result.Select(t => t.Content));
        }

        [Fact]
        public void DetailedCheck()
        {
            var result = Tokenizer.Tokenize("5romaji here...!?漢字ひらがなカタ　カナ４「ＳＨＩＯ」。！ لنذهب");
            var expectedResult = new Token[]
            {
                new Token(tokenType: TokenTypes.EnglishNumber, content: "5"),
                new Token(tokenType: TokenTypes.English , content: "romaji"),
                new Token(tokenType: TokenTypes.Space , content: " "),
                new Token(tokenType: TokenTypes.English, content: "here"),
                new Token(tokenType: TokenTypes.EnglishPunctuation, content: "...!?"),
                new Token(tokenType: TokenTypes.Kanji, content: "漢字"),
                new Token(tokenType: TokenTypes.Hiragana, content: "ひらがな"),
                new Token(tokenType: TokenTypes.Katakana, content: "カタ"),
                new Token(tokenType: TokenTypes.Space, content: "　"),
                new Token(tokenType: TokenTypes.Katakana, content: "カナ"),
                new Token(tokenType: TokenTypes.JapaneseNumber, content: "４"),
                new Token(tokenType: TokenTypes.JapanesePunctuation, content: "「"),
                new Token(tokenType: TokenTypes.Japanese, content: "ＳＨＩＯ"),
                new Token(tokenType: TokenTypes.JapanesePunctuation, content: "」。！"),
                new Token(tokenType: TokenTypes.Space, content: " "),
                new Token(tokenType: TokenTypes.Other, content: "لنذهب"),
            };
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CompactDetailedCheck()
        {
            var result = Tokenizer.Tokenize("5romaji here...!?漢字ひらがなカタ　カナ４「ＳＨＩＯ」。！ لنذهب", true);
            var expectedResult = new Token[]
            {
                new Token(tokenType: TokenTypes.Other, content: "5"),
                new Token(tokenType: TokenTypes.English , content: "romaji here"),
                new Token(tokenType: TokenTypes.Other, content: "...!?"),
                new Token(tokenType: TokenTypes.Japanese, content: "漢字ひらがなカタ　カナ"),
                new Token(tokenType: TokenTypes.Other, content: "４「"),
                new Token(tokenType: TokenTypes.Japanese, content: "ＳＨＩＯ"),
                new Token(tokenType: TokenTypes.Other, content: "」。！"),
                new Token(tokenType: TokenTypes.English, content: " "),
                new Token(tokenType: TokenTypes.Other, content: "لنذهب"),
            };
            Assert.Equal(expectedResult, result);
        }
    }
}
