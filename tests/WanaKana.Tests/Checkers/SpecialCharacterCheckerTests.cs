using WanaKanaNet.Characters;
using WanaKanaNet.Checkers;
using Xunit;

namespace WanaKanaNet.Tests.Checkers
{
    public class SpecialCharacterCheckerTests
    {
        [Fact]
        public void IsConsonantExcludingY()
        {
            Assert.False(SpecialCharacterChecker.IsConsonant('y', false));
        }

        [Fact]
        public void IsConsonantIncludesYByDefault()
        {
            Assert.True(SpecialCharacterChecker.IsConsonant('y'));
        }

        [Fact]
        public void IsConsonantRecognizesConsonants()
        {
            foreach (var consonant in CharacterRanges.Consonants)
            {
                Assert.True(SpecialCharacterChecker.IsConsonant(consonant));
            }
        }

        [InlineData('a')]
        [InlineData('!')]
        [Theory]
        public void IsConsonantInvalidCases(char character)
        {
            Assert.False(SpecialCharacterChecker.IsConsonant(character));
        }

        [Fact]
        public void IsVowelExcludingY()
        {
            Assert.False(SpecialCharacterChecker.IsVowel('y', false));
        }

        [Fact]
        public void IsVowelRecognizesVowels()
        {
            foreach (var vowel in CharacterRanges.Vowels)
            {
                Assert.True(SpecialCharacterChecker.IsVowel(vowel));
            }
        }

        [InlineData('x')]
        [InlineData('!')]
        [Theory]
        public void IsVowelInvalidCases(char character)
        {
            Assert.False(SpecialCharacterChecker.IsVowel(character));
        }

        [Fact]
        public void IsEnglishPunctuationAllEnglishPunctuationsAreRecognized()
        {
            foreach (var range in CharacterRanges.EnglishPunctuationRanges)
            {
                for (char character = range.Start; character <= range.End; character++)
                {
                    Assert.True(SpecialCharacterChecker.IsEnglishPunctuation(character));
                }
            }
        }

        [Fact]
        public void IsEnglishPunctuationAllJapanesePunctuationsAreInvalid()
        {
            foreach (var range in CharacterRanges.JapanesePunctuationRanges)
            {
                for (char character = range.Start; character <= range.End; character++)
                {
                    Assert.False(SpecialCharacterChecker.IsEnglishPunctuation(character));
                }
            }
        }

        [InlineData(' ', true)]
        [InlineData('a', false)]
        [InlineData('ふ', false)]
        [InlineData('字', false)]
        [Theory]
        public void IsEnglishPunctuationVarious(char character, bool expectedResult)
        {
            Assert.Equal(expectedResult, SpecialCharacterChecker.IsEnglishPunctuation(character));
        }

        [Fact]
        public void IsJapanesePunctuationAllJapanesePunctuationsAreRecognized()
        {
            foreach (var range in CharacterRanges.JapanesePunctuationRanges)
            {
                for (char character = range.Start; character <= range.End; character++)
                {
                    Assert.True(SpecialCharacterChecker.IsJapanesePunctuation(character));
                }
            }
        }

        [Fact]
        public void IsJapanesePunctuationAllEnglishPunctuationsAreInvalid()
        {
            foreach (var range in CharacterRanges.EnglishPunctuationRanges)
            {
                for (char character = range.Start; character <= range.End; character++)
                {
                    Assert.False(SpecialCharacterChecker.IsJapanesePunctuation(character));
                }
            }
        }

        [InlineData('　', true)]
        [InlineData('?', false)]
        [InlineData('a', false)]
        [InlineData('ふ', false)]
        [InlineData('字', false)]
        [Theory]
        public void IsJapanesePunctuationVarious(char character, bool expectedResult)
        {
            Assert.Equal(expectedResult, SpecialCharacterChecker.IsJapanesePunctuation(character));
        }
    }
}
