namespace ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Types.Type;

public record FunctionDeclarationStatement(Type ReturnType, ParameterList Parameters, Scope Scope) : Statement;