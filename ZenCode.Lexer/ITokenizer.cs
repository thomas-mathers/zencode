namespace ZenCode.Lexer;

public interface ITokenizer
{
    ITokenStream Tokenize(string text);
}