namespace ZenCode.Parser.Model.Types;

public record FunctionType(Type ReturnType, IReadOnlyList<Type> ParameterTypes) : Type;