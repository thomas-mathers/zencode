namespace ZenCode.Parser.Model.Grammar.Types;

public record FunctionType : Type
{
    private readonly Type _returnType;
    private readonly TypeList _parameterTypes = new();

    public required Type ReturnType
    {
        get => _returnType;
        init => _returnType = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TypeList ParameterTypes
    {
        get => _parameterTypes;
        init => _parameterTypes = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
        return $"({ParameterTypes}) => {ReturnType}";
    }
}
