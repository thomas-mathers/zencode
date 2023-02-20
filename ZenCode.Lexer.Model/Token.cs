namespace ZenCode.Lexer.Model;

public record Token(TokenType Type)
{
    public int EndingColumn => StartingColumn + Text.Length;
    public int Line { get; init; }
    public int StartingColumn { get; init; }
    public string Text { get; init; } = string.Empty;
}