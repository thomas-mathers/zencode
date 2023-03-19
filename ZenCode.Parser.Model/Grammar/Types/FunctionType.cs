namespace ZenCode.Parser.Model.Grammar.Types;

public record FunctionType(Type ReturnType, TypeList ParameterTypes) : Type
{
    public override string ToString()
    {
        return $"({ParameterTypes}) => {ReturnType}";
    }
}