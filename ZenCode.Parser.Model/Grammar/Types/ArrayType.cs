namespace ZenCode.Parser.Model.Grammar.Types;

public record ArrayType(Type BaseType) : Type
{
    public override string ToString()
    {
        return $"{BaseType}[]";
    }
}