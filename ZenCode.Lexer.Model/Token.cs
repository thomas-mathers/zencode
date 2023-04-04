namespace ZenCode.Lexer.Model;

public record Token(TokenType Type)
{
    public int EndingColumn => StartingColumn + Text.Length;
    public int Line { get; init; }
    public int StartingColumn { get; init; }
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