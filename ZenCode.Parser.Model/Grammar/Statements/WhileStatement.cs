namespace ZenCode.Parser.Model.Grammar.Statements;

public record WhileStatement : CompoundStatement
{
    public required ConditionScope ConditionScope { get; init; }

    public override string ToString()
    {
        return $"while {ConditionScope}";
    }
}
