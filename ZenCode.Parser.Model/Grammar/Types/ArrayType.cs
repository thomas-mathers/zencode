namespace ZenCode.Parser.Model.Grammar.Types;

public record ArrayType : Type
{
    public required Type BaseType { get; init; }

    public override string ToString()
    {
        return $"{BaseType}[]";
    }
}
