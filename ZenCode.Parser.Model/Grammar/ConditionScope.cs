using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar;

public record ConditionScope(Expression Condition, Scope Scope);