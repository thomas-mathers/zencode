using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ForStatement(Statement Initialization, Expression Condition, Statement Iterator, Scope Scope) : Statement;