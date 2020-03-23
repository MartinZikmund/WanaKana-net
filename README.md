<div align="center">
  <!-- NuGet Version -->
  <a href="https://www.nuget.org/packages/WanaKana-net/">
    <img src="https://img.shields.io/nuget/v/WanaKana-net" alt="NuGet package" />
  </a>
  <!-- Build Status -->
  <img src="https://dev.azure.com/martinzikmund/WanaKana-net/_apis/build/status/Master?branchName=master" alt="Build status" />
</div>

<div align="center">
<h1>ワナカナ &lt;--&gt; WanaKana-net &lt;--&gt; わなかな</h1>
<h4>.NET port of WanaKana.js - utility library for detecting and transliterating Hiragana, Katakana, and Romaji</h4>
</div>

## Usage

#### Install

```
dotnet add package WanaKana-net
```

## Documentation

## Quick Reference

```c#
/*** Text checking utilities ***/
WanaKana.IsJapanese("泣き虫。！〜２￥ｚｅｎｋａｋｕ")
// => true

WanaKana.IsKana("あーア")
// => true

WanaKana.IsHiragana("すげー")
// => true

WanaKana.IsKatakana("ゲーム")
// => true

WanaKana.IsKanji("切腹")
// => true
WanaKana.IsKanji("勢い")
// => false

WanaKana.IsRomaji("Tōkyō and Ōsaka")
// => true

/*** Conversion ***/

WanaKana.ToKana("ONAJI buttsuuji")
// => "オナジ ぶっつうじ"
WanaKana.ToKana("座禅‘zazen’スタイル")
// => "座禅「ざぜん」スタイル"
WanaKana.ToKana("batsuge-mu")
// => "ばつげーむ"
WanaKana.ToKana("WanaKana", 
   new WanaKanaOptions { 
      CustomKanaMapping = new Dictionary<string,string>(){ {"na", "に"}, {"ka", "bana" }} }) });
// => "わにbanaに"

WanaKana.ToHiragana("toukyou, オオサカ")
// => "とうきょう、　おおさか"
WanaKana.ToHiragana("only カナ", { passRomaji: true })
// => "only かな"
WanaKana.ToHiragana("wi", new WanaKanaOptions { UseObsoleteKana = true })
// => "ゐ"

WanaKana.ToKatakana("toukyou, おおさか")
// => "トウキョウ、　オオサカ"
WanaKana.ToKatakana("only かな", { passRomaji: true })
// => "only カナ"
WanaKana.ToKatakana("wi", new WanaKanaOptions { UseObsoleteKana = true })
// => "ヰ"

WanaKana.ToRomaji("ひらがな　カタカナ")
// => "hiragana katakana"
WanaKana.ToRomaji("ひらがな　カタカナ", new WanaKanaOptions { UppercaseKatakana = true })
// => "hiragana KATAKANA"
WanaKana.ToRomaji("つじぎり", 
   new WanaKanaOptions { 
      CustomRomajiMapping = new Dictionary<string,string>() { {"じ", "zi"}, {"つ", "tu"}, {"り", "li" }}}) };
// => "tuzigili"

/*** EXTRA UTILITIES ***/
WanaKana.StripOkurigana("お祝い")
// => "お祝"
WanaKana.StripOkurigana("踏み込む")
// => "踏み込"
WanaKana.StripOkurigana("お腹", leading: true });
// => "腹"
WanaKana.StripOkurigana("ふみこむ", new WanaKanaOptions { MatchKanji = "踏み込む" });
// => "ふみこ"
WanaKana.StripOkurigana("おみまい", new WanaKanaOptions { MatchKanji = "お祝い", Leading = true });
// => "みまい"

WanaKana.Tokenize("ふふフフ")
// => ["ふふ", "フフ"]
WanaKana.Tokenize("hello 田中さん")
// => ["hello", " ", "田中", "さん"]
WanaKana.Tokenize("I said 私はすごく悲しい", compact: true })
// => [ "I said ", "私はすごく悲しい"]
```