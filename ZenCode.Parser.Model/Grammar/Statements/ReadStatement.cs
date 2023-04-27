using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ReadStatement : SimpleStatement
{
    public required VariableReferenceExpression VariableReference { get; init; }

    public override string ToString()
    {
        return $"read {VariableReference}";
    }
}
