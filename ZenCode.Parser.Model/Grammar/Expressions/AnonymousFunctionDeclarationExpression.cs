using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record AnonymousFunctionDeclarationExpression(Type ReturnType, ParameterList Parameters, Scope Scope) : Expression;