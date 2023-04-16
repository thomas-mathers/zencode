namespace ZenCode.Lexer.Exceptions;

public class TokenParseException : LexerException
{
    public TokenParseException(int line, int column) : base
    (
        line,
        column,
        $"Unable to parse token on line {line} and column {column}"
    )
    {
    }
}
