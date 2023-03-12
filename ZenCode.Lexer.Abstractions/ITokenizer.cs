namespace ZenCode.Lexer.Abstractions
{
    public interface ITokenizer
    {
        ITokenStream Tokenize(string text);
    }
}