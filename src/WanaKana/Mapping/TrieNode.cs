using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Mapping
{
    public class TrieNode<TKey, TValue>
    {
        private readonly Dictionary<TKey, TrieNode<TKey, TValue>> _children = 
            new Dictionary<TKey, TrieNode<TKey, TValue>>();

        public TrieNode(TKey key, TValue value, TrieNode<TKey, TValue>? parent = null)
        {
            Key = key;
            Value = value;
            Parent = parent;
        }

        public TrieNode<TKey, TValue>? Parent { get; }

        public TKey Key { get; }

        public TValue Value { get; }

        public IReadOnlyDictionary<TKey, TrieNode<TKey, TValue>> Children => _children;

        public void Accept(ITrieVisitor<TKey, TValue> visitor)
        {
            visitor.Visit(this);
        }
    }
}
