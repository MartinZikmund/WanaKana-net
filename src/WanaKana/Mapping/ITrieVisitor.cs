using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Mapping
{
    public interface ITrieVisitor<TKey,TValue>
    {
        void Visit(TrieNode<TKey, TValue> trieNode);
    }
}
