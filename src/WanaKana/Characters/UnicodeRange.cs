namespace WanaKanaNet.Characters
{
    /// <summary>
    /// Represents a range of Unicode characters.
    /// </summary>
    internal class UnicodeRange
    {
        /// <summary>
        /// Initializes an instance of UnicodeRange with given bounds.
        /// </summary>
        /// <param name="start">Inclusive start of the interval.</param>
        /// <param name="end">Exclusive end of the interval</param>
        public UnicodeRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Inclusive start of the interval.
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// Exclusive end of the interval.
        /// </summary>
        public int End { get; }
    }
}
