namespace ZenCode.Parser.Model.Grammar.Types;

public record FunctionType : Type
{
    public FunctionType(Type returnType, TypeList parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(returnType);
        ArgumentNullException.ThrowIfNull(parameterTypes);

        ReturnType = returnType;
        ParameterTypes = parameterTypes;
    }

    public Type ReturnType { get; init; }
    public TypeList ParameterTypes { get; init; }

    public override string ToString()
    {
        return $"({ParameterTypes}) => {ReturnType}";
    }
}
