using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record AssignmentStatement
    (VariableReferenceExpression VariableReferenceExpression, Expression Expression) : Statement;