namespace ZenCode.Lexer.Model;

public record Token
{
    public int EndingColumn => StartingColumn + Text.Length;
    public int Line { get; init; }
    public int StartingColumn { get; init; }
    public string Text { get; init; } = string.Empty;
    public required TokenType Type { get; init; }
}