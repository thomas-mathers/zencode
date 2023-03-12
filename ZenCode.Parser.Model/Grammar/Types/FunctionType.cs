namespace ZenCode.Parser.Model.Grammar.Types;

public record FunctionType(Type ReturnType, TypeList ParameterTypes) : Type;