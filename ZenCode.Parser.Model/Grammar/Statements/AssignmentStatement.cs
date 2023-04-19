using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record AssignmentStatement : SimpleStatement
{
    public required VariableReferenceExpression VariableReference { get; init; }
    public required Expression Value { get; init; }

    public override string ToString()
    {
        return $"{VariableReference} := {Value}";
    }
}
