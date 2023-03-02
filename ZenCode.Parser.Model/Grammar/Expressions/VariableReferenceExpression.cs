using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record VariableReferenceExpression(Token Identifier) : Expression
{
    public ExpressionList Indices { get; init; } = new();
}