namespace ZenCode.Parser.Model.Grammar.Types;

public record ArrayType : Type
{
    public Type BaseType { get; init; }

    public ArrayType(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);
        
        BaseType = baseType;
    }
    
    
    public override string ToString()
    {
        return $"{BaseType}[]";
    }
}