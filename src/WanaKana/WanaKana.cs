using System;

namespace WanaKanaNet
{
    public static class WanaKana
    {
        public static string ToHiragana(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            return string.Empty;
        }
    }
}
