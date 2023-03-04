using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record FunctionDeclarationStatement(Type ReturnType, ParameterList Parameters, Scope Scope) : Statement;