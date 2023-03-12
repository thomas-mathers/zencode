using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements
{
    public record AssignmentStatement
        (Expression VariableReferenceExpression, Expression Expression) : Statement;
}