using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Mapping
{
    public class TraverseTrieVisitor<TKey, TValue> : ITrieVisitor<TKey, TValue>
    {
        private readonly string _prefix;

        public TraverseTrieVisitor(string prefix)
        {
            _prefix = prefix;
        }

        public void Visit(TrieNode<TKey, TValue> trieNode)
        {
            //trieNode.Key

            //foreach (var child in trieNode.Children.Values)
            //{
            //    child.Accept(new TraverseTrieVisitor(_prefix));
            //}
            throw new NotImplementedException();
        }
    }
}
