namespace ZenCode.Parser.Model.Grammar.Types;

public record ArrayType : Type
{
    public ArrayType(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        BaseType = baseType;
    }

    public Type BaseType { get; init; }


    public override string ToString()
    {
        return $"{BaseType}[]";
    }
}