using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record VariableReferenceExpression(Token Identifier) : Expression
{
    public ArrayIndexExpressionList Indices { get; init; } = new();

    public override string ToString()
    {
        return $"{Identifier}{Indices}";
    }
}