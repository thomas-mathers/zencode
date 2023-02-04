namespace ZenCode.Lexer;

public interface ITokenizer
{
    IEnumerable<Token> Tokenize(string text);
}