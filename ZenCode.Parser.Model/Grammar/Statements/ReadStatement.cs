using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ReadStatement(VariableReferenceExpression VariableReferenceExpression) : SimpleStatement;