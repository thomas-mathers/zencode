using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record AssignmentStatement : SimpleStatement
{
    public required VariableReferenceExpression Variable { get; init; }
    public required Expression Value { get; init; }

    public override string ToString()
    {
        return $"{Variable} := {Value}";
    }
}
