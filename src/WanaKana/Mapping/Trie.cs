using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WanaKanaNet.Mapping
{
    public class Trie
    {
        public TrieNode Root { get; } = new TrieNode(default!, default!);

        public Trie Union(Trie second)
        {
            var trie = new Trie();
            var root = trie.Root;

            return trie;
        }

        // transform the tree, so that for example hepburnTree['ゔ']['ぁ'][''] === 'va'
        // or kanaTree['k']['y']['a'][''] === 'きゃ'
        public static Trie FromDictionary(IReadOnlyDictionary<string, string> dictionary)
        {
            var trie = new Trie();
            foreach (var pair in dictionary)
            {
                trie[pair.Key] = pair.Value;
            }
            return trie;
        }

        private string? GetValue(string key)
        {
            var node = GetNode(key, false);
            return node?.Value;
        }

        public void SetValue(string key, string? value)
        {
            var currentNode = Root;
            for (int i = 0; i < key.Length; i++)
            {
                var currentCharacter = key[i];
                if (currentNode.Children.TryGetValue(currentCharacter, out var node))
                {
                    currentNode = node;
                }
                else
                {
                    currentNode.AddChild(currentCharacter, i == key.Length - 1 ? value : null);
                }
            }
            currentNode.Value = value;
        }

        public string? this[string key]
        {
            get => GetValue(key);
            set => SetValue(key, value);
        }

        private TrieNode? GetNode(string key, bool createPath)
        {
            var currentNode = Root;
            for (int i = 0; i < key.Length; i++)
            {
                var currentCharacter = key[i];
                if (!currentNode.Children.TryGetValue(currentCharacter, out var nextNode))
                {
                    if (createPath)
                    {
                        nextNode = currentNode.AddChild(currentCharacter);
                    }
                    else
                    {
                        return null;
                    }
                }
                currentNode = nextNode;
            }
            return currentNode;
        }

        internal void InsertSubtrie(string prefix, Trie trie)
        {
            
        }

        internal Trie GetSubtrie(string key)
        {
            return null;
        }

        internal Trie Clone()
        {
            return null;
        }
    }
}
