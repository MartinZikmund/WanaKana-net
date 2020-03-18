using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Characters;

namespace WanaKanaNet.Checkers
{
    public static class SpecialCharacterCheckers
    {
        public static bool IsLongDash(char character) => 
            character == CharacterBounds.ProlongedSoundMark;
    }
}
