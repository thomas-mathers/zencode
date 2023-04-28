namespace ZenCode.Parser.Model.Grammar.Types;

public record FunctionType : Type
{
    public required Type ReturnType { get; init; }

    public TypeList ParameterTypes { get; init; } = new();

    public override string ToString()
    {
        return $"({ParameterTypes}) => {ReturnType}";
    }
}
