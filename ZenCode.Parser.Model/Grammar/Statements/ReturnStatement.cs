using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ReturnStatement : SimpleStatement
{
    public Expression? Expression { get; init; } = null;

    public override string ToString()
    {
        return $"return {Expression}";
    }
}