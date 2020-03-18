using System;
using WanaKanaNet.Converters;

namespace WanaKanaNet
{
    public static class WanaKana
    {
        public static string ToHiragana(string input) => HiraganaConverter.ToHiragana(input);
    }
}
