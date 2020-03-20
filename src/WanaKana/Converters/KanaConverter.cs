using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Enums;

namespace WanaKanaNet.Converters
{
    internal static class KanaConverter
    {
        public static string ToKana(string input, WanaKanaOptions? options = null, IReadOnlyDictionary<string, string>? map = null)
        {
            options ??= new WanaKanaOptions();
            map ??= CreateRomajiToKanaMap(options);

            throw new NotImplementedException();
        }

        private static IReadOnlyDictionary<string, string> CreateRomajiToKanaMap(WanaKanaOptions options)
        {
            //var map = GetRomajiToKanaTree();

            //map = options.ImeMode != ImeMode.None ? IME_MODE_MAP(map) : map;
            //map = options.UseObsoleteKana ? USE_OBSOLETE_KANA_MAP(map) : map;

            //if (options.CustomKanaMapping != null)
            //{
            //    if (customMapping == null)
            //    {
            //        customMapping = mergeCustomMapping(map, options.customKanaMapping);
            //    }
            //    map = customMapping;
            //}

            //return map;
            throw new NotImplementedException();
        }

        private static string SplitIntoConvertedKana(string input, WanaKanaOptions? options, IReadOnlyDictionary<string, string>? map)
        {
            options ??= new WanaKanaOptions();
            if (map == null)
            {
                map = CreateRomajiToKanaMap(options);
            }

            //return ApplyMapping(input.ToLowerInvariant(), map, options.ImeMode == ImeMode.None);
            throw new NotImplementedException();
        }
    }
}
