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
        /// <param name="end">Inclusive end of the interval</param>
        public UnicodeRange(char start, char end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Inclusive start of the interval.
        /// </summary>
        public char Start { get; }

        /// <summary>
        /// Inclusive end of the interval.
        /// </summary>
        public char End { get; }

        public bool Contains(char character)
        {
            return Start <= character && character <= End;
        }
    }
}
