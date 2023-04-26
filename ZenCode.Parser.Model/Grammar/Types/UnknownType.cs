namespace ZenCode.Parser.Model.Grammar.Types;

public record UnknownType : Type
{
    public override string ToString()
    {
        return "??";
    }
}
