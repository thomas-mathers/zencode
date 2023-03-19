using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar;

public record ConditionScope(Expression Condition, Scope Scope) : AstNode
{
    public override string ToString()
    {
        return $"({Condition}) {Scope}";
    }
}