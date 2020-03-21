using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WanaKanaNet.Helpers;

namespace WanaKanaNet.Mapping
{
    internal static class TrieHelpers
    {
        internal static SplitToken[] ApplyTrie(string input, Trie trie, bool convertEnding)
        {
            var root = trie.Root;

            TrieNode? GetNextSubtree(TrieNode node, char nextChar)
            {
                if (!node.Children.TryGetValue(nextChar, out var child))
                {
                    return null;
                }
                return child;
            }

            SplitToken[] NewChunk(string remaining, int currentCursor)
            {
                // start parsing a new chunk
                var firstChar = remaining[0];

                if (!root.Children.TryGetValue(firstChar, out var child))
                {
                    child = new TrieNode(firstChar, firstChar.ToString());
                }
                return Parse(
                    child,
                    remaining.Substring(1),
                    currentCursor,
                    currentCursor + 1
                );
            }

            SplitToken[] Parse(TrieNode tree, string remaining, int lastCursor, int currentCursor)
            {
                if (string.IsNullOrEmpty(remaining))
                {
                    if (convertEnding || tree.Children.Count == 0)
                    {
                        // nothing more to consume, just commit the last chunk and return it
                        // so as to not have an empty element at the end of the result
                        return new[] { new SplitToken(lastCursor, currentCursor, tree.Value) };
                    }
                    // if we don't want to convert the ending, because there are still possible continuations
                    // return null as the final node value
                    return new[] { new SplitToken(lastCursor, currentCursor, null) };
                }

                if (tree.Children.Count == 0)
                {
                    return new[] { new SplitToken(lastCursor, currentCursor, tree.Value) }.Concat(NewChunk(remaining, currentCursor)).ToArray();
                }

                var subtree = GetNextSubtree(tree, remaining[0]);

                if (subtree == null)
                {
                    return new[] { new SplitToken(lastCursor, currentCursor, tree.Value ?? input.Substring(lastCursor, currentCursor - lastCursor)) }.Concat(NewChunk(remaining, currentCursor)).ToArray();
                }
                // continue current branch
                return Parse(subtree, remaining.Substring(1), lastCursor, currentCursor + 1);
            }

            return NewChunk(input, 0);
        }
    }
}
