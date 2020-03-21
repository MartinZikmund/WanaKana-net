using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WanaKanaNet.Helpers
{
    public struct SplitToken
    {
        public SplitToken(int start, int end, string? content)
        {
            Start = start;
            End = end;
            Content = content;
        }

        public void Deconstruct(out int start, out int end, out string? content) =>
            (start, end, content) = (Start, End, Content);

        public int Start { get; }

        public int End { get; }

        public string? Content { get; }
    }
}
