namespace ZenCode.Parser.Model.Grammar.Types;

public record ArrayType : Type
{
    private readonly Type _baseType;

    public required Type BaseType
    {
        get => _baseType;
        init => _baseType = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
        return $"{BaseType}[]";
    }
}
