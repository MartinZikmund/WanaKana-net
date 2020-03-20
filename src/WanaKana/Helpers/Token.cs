namespace WanaKanaNet.Helpers
{
    public struct Token
    {
        public Token(char character, string tokenType) :
            this(character.ToString(), tokenType)
        {
        }

        public Token(string content, string tokenType)
        {
            TokenType = tokenType;
            Content = content;
        }

        public string TokenType { get; }

        public string Content { get; }

        public Token Extend(char character) =>
            new Token(Content + character, TokenType);
    }
}
