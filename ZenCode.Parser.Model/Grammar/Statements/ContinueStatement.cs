namespace ZenCode.Parser.Model.Grammar.Statements;

public record ContinueStatement : SimpleStatement
{
    public override string ToString()
    {
        return "continue";
    }
}
