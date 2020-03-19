using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Enums;

namespace WanaKanaNet.Converters
{
    internal static class KanaConverters
    {
        public static string ToKana(string input, WanaKanaOptions? options = null, IReadOnlyDictionary<string, string> customMapping)
        {
            options ??= new WanaKanaOptions();
            customMapping ??= CreateRomajiToKanaMap(options);


        }

        private static IReadOnlyDictionary<string, string> CreateRomajiToKanaMap(WanaKanaOptions options)
        {
            var map = GetRomajiToKanaTree();

            map = options.ImeMode != ImeMode.None ? IME_MODE_MAP(map) : map;
            map = options.UseObsoleteKana ? USE_OBSOLETE_KANA_MAP(map) : map;

            if (options.CustomKanaMapping != null)
            {
                if (customMapping == null)
                {
                    customMapping = mergeCustomMapping(map, options.customKanaMapping);
                }
                map = customMapping;
            }

            return map;
        }
    }
}
