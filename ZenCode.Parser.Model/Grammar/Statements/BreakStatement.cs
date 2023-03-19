namespace ZenCode.Parser.Model.Grammar.Statements;

public record BreakStatement : SimpleStatement
{
    public override string ToString()
    {
        return "break";
    }
}