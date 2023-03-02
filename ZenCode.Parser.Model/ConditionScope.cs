using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model;

public record ConditionScope(Expression Condition, Scope Scope);