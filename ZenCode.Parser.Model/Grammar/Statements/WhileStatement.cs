namespace ZenCode.Parser.Model.Grammar.Statements;

public record WhileStatement(ConditionScope ConditionScope) : CompoundStatement
{
    public override string ToString()
    {
        return $"while {ConditionScope}";
    }
}