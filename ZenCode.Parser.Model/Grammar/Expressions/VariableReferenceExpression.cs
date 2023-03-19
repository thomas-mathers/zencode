using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record VariableReferenceExpression(Token Identifier) : Expression
{
    public ExpressionList Indices { get; init; } = new();

    public override string ToString()
    {
        return Indices.Expressions.Count > 0 ? $"{Identifier}[{Indices}]" : Identifier.ToString();
    }
}