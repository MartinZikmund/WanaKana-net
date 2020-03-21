using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Mapping
{
    internal class TrieNode
    {
        private readonly Dictionary<char, TrieNode> _children =
            new Dictionary<char, TrieNode>();

        public TrieNode(char key, string? value, TrieNode? parent = null)
        {
            Key = key;
            Value = value;
            Parent = parent;
        }

        public TrieNode? Parent { get; }

        public char Key { get; }

        public string? Value { get; set; }

        public IReadOnlyDictionary<char, TrieNode> Children => _children;

        public TrieNode AddChild(char key, TrieNode node)
        {
            return (_children[key] = node);
        }

        internal void DeleteChild(char key)
        {
            _children.Remove(key);
        }

        public TrieNode AddChild(char key, string? value = null)
        {
            return (_children[key] = new TrieNode(key, value, this));
        }
    }
}
