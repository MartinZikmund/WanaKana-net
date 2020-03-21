using System.Collections.Generic;

namespace WanaKanaNet
{
    /// <summary>
    /// Represents conversion configuration options for WanaKana.
    /// </summary>
    public class WanaKanaOptions
    {
        /// <summary>
        /// Set to true to use obsolete characters, such as ゐ and ゑ.
        /// <example>
        /// ToHiragana('we', new WanaKanaOptions { UseObsoleteKana = true }) => 'ゑ'
        /// </example>
        /// </summary>
        public bool UseObsoleteKana { get; set; } = false;

        /// <summary>
        /// Set to true to pass romaji when using mixed syllables with ToKatakana() or ToHiragana().
        /// <example>
        /// ToHiragana('we', new WanaKanaOptions { PassRomaji = true }) => => "only convert the katakana: ひらがな"
        /// </example>
        /// </summary>
        public bool PassRomaji { get; set; } = false;

        /// <summary>
        /// Set to true to convert katakana to uppercase using ToRomaji()
        /// <example>
        /// ToRomaji('ひらがな カタカナ', new WanaKanaOptions { UppercaseKatakana = true }) => "hiragana KATAKANA"
        /// </example>
        /// </summary>
        public bool UppercaseKatakana { get; set; } = false;

        /// <summary>
        /// Set to true, 'ToHiragana', or 'ToKatakana' to handle conversion while it is being typed.
        /// </summary>
        public ImeMode ImeMode { get; set; } = ImeMode.None;

        /// <summary>
        /// Choose ToRomaji() romanization map (currently only 'Hepburn').
        /// </summary>
        public RomanizationType Romanization { get; set; } = RomanizationType.Hepburn;

        /// <summary>
        /// Custom map will be merged with default conversion.
        /// <example>
        /// ToKana('wanakana', new WanaKanaOptions { CustomKanaMapping = { { na, 'に'}, { ka, 'Bana'} }) => 'わにBanaに'
        /// </example>
        /// </summary>
        public IDictionary<string, string> CustomKanaMapping { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Custom map will be merged with default conversion.
        /// <example>
        /// ToRomaji('つじぎり', new WanaKanaOptions { CustomRomajiMapping = { { じ, 'zi'}, { つ, 'tu'}, {り, 'li'} }) }) => 'tuzigili'
        /// </example>
        /// </summary>
        public IDictionary<string, string> CustomRomajiMapping { get; set; } = new Dictionary<string, string>();
    }
}
