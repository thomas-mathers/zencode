namespace ZenCode.Parser.Model.Grammar.Types;

public record FunctionType(Type ReturnType, IReadOnlyList<Type> ParameterTypes) : Type;