using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Helpers
{
    public struct SplitToken
    {
        public SplitToken(string content, int start, int end)
        {
            Content = content;
            Start = start;
            End = end;
        }

        public void Deconstruct(out int start, out int end, out string content) =>
            (start, end, content) = (Start, End, Content);

        public string Content { get; }

        public int Start { get; }

        public int End { get; }
    }
}
