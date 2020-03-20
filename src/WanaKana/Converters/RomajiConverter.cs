using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Checkers;
using WanaKanaNet.Helpers;

namespace WanaKanaNet.Converters
{
    internal static class RomajiConverter
    {
        public static string ToRomaji(string input, WanaKanaOptions? options = null)
        {
            options ??= new WanaKanaOptions();

            // just throw away the substring index information and just concatenate all the kana
            var romajiTokens = SplitIntoRomaji(input, options);
            var builder = new StringBuilder();
            foreach (var token in romajiTokens)
            {
                var (start, end, romaji) = token;
                var makeUppercase = options.UppercaseKatakana && KatakanaChecker.IsKatakana(input.Substring(start, end - start));
                builder.Append(makeUppercase ? romaji.ToUpperInvariant() : romaji);
            }
            return builder.ToString();
        }

        private static IDictionary<string, string> customMapping = null;
        
        private static SplitToken[] SplitIntoRomaji(string input, WanaKanaOptions options)
        {
            //var map = GetKanaToRomajiTree(options);

            //if (options.customRomajiMapping)
            //{
            //    if (customMapping == null)
            //    {
            //        customMapping = mergeCustomMapping(map, options.customRomajiMapping);
            //    }
            //    map = customMapping;
            //}

            //return applyMapping(katakanaToHiragana(input, toRomaji, true), map, !options.IMEMode);
            throw new NotImplementedException();
        }
    }
}
