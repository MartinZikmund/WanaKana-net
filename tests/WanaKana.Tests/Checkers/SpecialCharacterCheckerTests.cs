using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void IsConsonantRecognizesConsonants()
        {
            foreach (var consonant in CharacterBounds.Consonants)
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
    }
}
