using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record AssignmentStatement
    (VariableReferenceExpression VariableReferenceExpression, Expression Expression) : SimpleStatement
{
    public override string ToString()
    {
        return $"{VariableReferenceExpression} := {Expression}";
    }
}
