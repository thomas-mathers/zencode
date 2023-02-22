namespace ZenCode.Grammar.Statements;

public record IfStatement(ConditionScope ThenScope) : Statement
{
    public IReadOnlyList<ConditionScope> ElseIfScopes { get; init; } = Array.Empty<ConditionScope>();
    public Scope? ElseScope { get; init; }
}