using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WanaKanaNet.Mapping
{
    internal class Trie
    {
        public Trie()
        {
        }

        public Trie(TrieNode root)
        {
            Root = root;
        }

        public TrieNode Root { get; } = new TrieNode(default!, default!);

        public Trie Union(Trie second)
        {
            void InDepth(TrieNode target, TrieNode source)
            {
                target.Value = source.Value;
                foreach (var sourceChild in source.Children)
                {
                    if (!target.Children.TryGetValue(sourceChild.Key, out var targetChild))
                    {
                        targetChild = target.AddChild(sourceChild.Key);
                    }
                    InDepth(targetChild, sourceChild.Value);
                }
            }
            var trie = new Trie();
            InDepth(trie.Root, Root);
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
                    currentNode = currentNode.AddChild(currentCharacter);
                }
            }
            currentNode.Value = value;
        }

        internal void AddRange(IDictionary<string, string> customRomajiMapping)
        {
            foreach (var entry in customRomajiMapping)
            {
                this[entry.Key] = entry.Value;
            }
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
            var node = GetNode(prefix, true);
            foreach (var child in trie.Root.Children)
            {
                node!.AddChild(child.Key, child.Value);
            }
        }

        internal void AssignSubtrie(string prefix, Trie trie)
        {
            var node = GetNode(prefix, true);            
            node.Value = trie.Root.Value;
            foreach (var child in trie.Root.Children)
            {
                node!.AddChild(child.Key, child.Value);
            }
        }

        internal Trie GetSubtrie(string key)
        {
            return new Trie(GetNode(key, true)!);
        }

        internal Trie Clone()
        {
            void InDepth(TrieNode target, TrieNode source)
            {                
                target.Value = source.Value;
                foreach (var sourceChild in source.Children)
                {
                    var targetNode = target.AddChild(sourceChild.Key);
                    InDepth(targetNode, sourceChild.Value);
                }
            }
            var trie = new Trie(new TrieNode(Root.Key, Root.Value));
            InDepth(trie.Root, Root);
            return trie;
        }

        internal IEnumerable<KeyValuePair<string, string>> GetEntries()
        {
            IEnumerable<KeyValuePair<string, string>> InDepth(string prefix, TrieNode node)
            {
                if (node.Value != null)
                {
                    yield return new KeyValuePair<string, string>(prefix, node.Value);
                }
                foreach (var child in node.Children)
                {
                    foreach (var result in InDepth(prefix + child.Key, child.Value))
                    {
                        yield return result;
                    }
                }
            }
            return InDepth(string.Empty, Root);
        }
    }
}
