using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record FunctionDeclarationStatement(Type ReturnType, ParameterList Parameters, Scope Scope) : CompoundStatement;