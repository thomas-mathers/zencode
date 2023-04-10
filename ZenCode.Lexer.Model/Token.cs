namespace ZenCode.Lexer.Model;

public record Token
{
    public Token(TokenType type)
    {
        Type = type;
    }

    public Token(TokenType type, string text)
    {
        Type = type;
        Text = text;
    }

    public TokenType Type { get; init; }
    public int Line { get; init; }
    public int StartingColumn { get; init; }
    public int EndingColumn => StartingColumn + Text.Length;
    public string Text { get; init; } = string.Empty;

    public virtual bool Equals(Token? other)
    {
        return other != null && Type == other.Type;
    }

    public override string ToString()
    {
        return Type is TokenType.Identifier or TokenType.BooleanLiteral or TokenType.IntegerLiteral
            or TokenType.FloatLiteral or TokenType.StringLiteral
            ? Text
            : Type.ToString();
    }

    public override int GetHashCode()
    {
        return Type.GetHashCode();
    }
}