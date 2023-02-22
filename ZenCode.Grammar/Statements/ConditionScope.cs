using ZenCode.Grammar.Expressions;

namespace ZenCode.Grammar.Statements;

public record ConditionScope(Expression Condition, Scope Scope) : Statement;