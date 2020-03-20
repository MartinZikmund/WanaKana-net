using System;
using System.Collections.Generic;
using System.Text;

namespace WanaKanaNet.Mapping
{
    public class Trie<TKey, TValue>
    {
        public TrieNode<TKey, TValue> Root { get; } = new TrieNode<TKey, TValue>(default!, default!);

        public Trie<TKey, TValue> Union(Trie<TKey, TValue> second)
        {
            var trie = new Trie<TKey, TValue>();
            var root = trie.Root;



            return trie;
        }
    }
}
