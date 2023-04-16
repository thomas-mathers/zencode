namespace ZenCode.Lexer.Exceptions;

public abstract class LexerException : Exception
{
    public int? Line { get; }
    public int? Column { get; }
    
    public LexerException()
    {
    }

    public LexerException(string message) : base(message)
    {
    }
    
    public LexerException(int line, int column, string message) : base(message)
    {
        Line = line;
        Column = column;
    }
}
