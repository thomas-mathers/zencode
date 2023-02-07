﻿namespace ZenCode.Lexer;

public record Token
{
    public required TokenType Type { get; init; }
    public int Line { get; init; }
    public int StartingColumn { get; init; }
    public string Text { get; init; } = string.Empty;
    public int EndingColumn => StartingColumn + Text.Length;
}