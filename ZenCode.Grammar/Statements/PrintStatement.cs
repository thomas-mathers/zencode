using ZenCode.Grammar.Expressions;

namespace ZenCode.Grammar.Statements;

public record PrintStatement(Expression Expression) : Statement;