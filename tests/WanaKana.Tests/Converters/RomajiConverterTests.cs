using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;
using WanaKanaNet.Converters;
using WanaKanaNet.Tests.Helpers;
using Xunit;

namespace WanaKanaNet.Tests.Converters
{
    public class RomajiConverterTests
    {
        [Fact]
        public void NullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => WanaKana.ToRomaji(null!));
        }

        [Fact]
        public void EmptyResultIsEmpty()
        {
            Assert.Empty(WanaKana.ToRomaji(string.Empty));
        }

        [InlineData("ワニカニ　ガ　スゴイ　ダ", "wanikani ga sugoi da")] //katakana
        [InlineData("わにかに　が　すごい　だ", "wanikani ga sugoi da")] //hiragana
        [InlineData("ワニカニ　が　すごい　だ", "wanikani ga sugoi da")] //mixed kana
        [Theory]
        public void ToRomajiBasicTests(string input, string expectedResult)
        {
            var result = WanaKana.ToRomaji(input);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ToRomajiPunctuation()
        {
            var result = WanaKana.ToRomaji(string.Join(string.Empty, ConversionTables.JapanesePunctuation));
            Assert.Equal(string.Join(string.Empty, ConversionTables.EnglishPunctuation), result);
        }

        [Fact]
        public void ToRomajiWithUppercasing()
        {
            var result = WanaKana.ToRomaji("ワニカニ", new WanaKanaOptions() { UppercaseKatakana = true });
            Assert.Equal("WANIKANI", result);
        }

        [Fact]
        public void ToRomajiWithUppercasingMixed()
        {
            var result = WanaKana.ToRomaji("ワニカニ　が　すごい　だ", new WanaKanaOptions() { UppercaseKatakana = true });
            Assert.Equal("WANIKANI ga sugoi da", result);
        }

        [Fact]
        public void ToRomajiLongDashToHyphen()
        {
            var result = WanaKana.ToRomaji("ばつげーむ");
            Assert.Equal("batsuge-mu", result);
        }

        [Fact]
        public void DashLikeKanji()
        {
            var result = WanaKana.ToRomaji("一抹げーむ");
            Assert.Equal("一抹ge-mu", result);
        }
        
        [Fact]
        public void ChonpuToLongVowel()
        {
            var result = WanaKana.ToRomaji("スーパー");
            Assert.Equal("suupaa", result);
        }

        [Fact]
        public void KatakanaSpecialOu()
        {
            var result = WanaKana.ToRomaji("缶コーヒー");
            Assert.Equal("缶koohii", result);
        }

        [Fact]
        public void SpacesNotAdded()
        {
            var result = WanaKana.ToRomaji("わにかにがすごいだ");
            Assert.NotEqual("wanikani ga sugoi da", result);
        }

        [InlineData("きんにくまん", "kinnikuman")]
        [InlineData("んんにんにんにゃんやん", "nnninninnyan'yan")]
        [InlineData("かっぱ　たった　しゅっしゅ ちゃっちゃ　やっつ", "kappa tatta shusshu chatcha yattsu")]
        [Theory]
        public void DoubleNsAndDoubleConsonants(string input, string expectedResult)
        {
            var result = WanaKana.ToRomaji(input);
            Assert.Equal(expectedResult, result);
        }

        [InlineData("っ","")]
        [InlineData("ヶ", "ヶ")]
        [InlineData("ヵ", "ヵ")]
        [InlineData("ゃ", "ya")]
        [InlineData("ゅ", "yu")]
        [InlineData("ょ", "yo")]
        [InlineData("ぁ", "a")]
        [InlineData("ぃ", "i")]
        [InlineData("ぅ", "u")]
        [InlineData("ぇ", "e")]
        [InlineData("ぉ", "o")]
        [Theory]
        public void SmallKana(string input, string expectedResult)
        {
            var result = WanaKana.ToRomaji(input);
            Assert.Equal(expectedResult, result);
        }

        [InlineData("おんよみ", "on'yomi")]
        [InlineData("んよ んあ んゆ", "n'yo n'a n'yu")]
        [InlineData("シンヨ", "shin'yo")]
        [Theory]
        public void ApostrophesInAmbiguousConsonantVowelCombos(string input, string expectedResult)
        {
            var result = WanaKana.ToRomaji(input);
            Assert.Equal(expectedResult, result);
        }
    }
}
