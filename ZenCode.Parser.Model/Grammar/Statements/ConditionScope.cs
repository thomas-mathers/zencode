using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ConditionScope(Expression Condition, Scope Scope) : Statement;