﻿using System;
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
                // if the next child node does not have a node value, set its node value to the input
                //node.Children[nextChar].Value = node.Value != null ? node.Value + nextChar : null;
                return node.Children[nextChar];
            }

            SplitToken[] NewChunk(string remaining, int currentCursor)
            {
                // start parsing a new chunk
                var firstChar = remaining[0];

                return Parse(
                    /*{ '': firstChar }, */root.Children[firstChar],
                    remaining.Substring(1),
                    currentCursor,
                    currentCursor + 1
                );
            }

            SplitToken[] Parse(TrieNode tree, string remaining, int lastCursor, int currentCursor)
            {
                if (string.IsNullOrEmpty(remaining))
                {
                    if (convertEnding || tree.Value != null)
                    {
                        // nothing more to consume, just commit the last chunk and return it
                        // so as to not have an empty element at the end of the result
                        return tree.Value != null ?
                            new[] { new SplitToken(tree.Value, lastCursor, currentCursor) } :
                            Array.Empty<SplitToken>();
                    }
                    // if we don't want to convert the ending, because there are still possible continuations
                    // return null as the final node value
                    return new[] { new SplitToken(null, lastCursor, currentCursor) };
                }

                if (tree.Value != null)
                {
                    return new[] { new SplitToken(tree.Value, lastCursor, currentCursor) }.Concat(NewChunk(remaining, currentCursor)).ToArray();
                }

                var subtree = GetNextSubtree(tree, remaining[0]);

                if (subtree == null)
                {
                    return new[] { new SplitToken(tree.Value, lastCursor, currentCursor) }.Concat(NewChunk(remaining, currentCursor)).ToArray();
                }
                // continue current branch
                return Parse(subtree, remaining.Substring(1), lastCursor, currentCursor + 1);
            }

            return NewChunk(input, 0);
        }
    }
}
