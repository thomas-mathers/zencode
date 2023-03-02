using ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record AnonymousFunctionDeclarationExpression
    (Type ReturnType, IReadOnlyList<Parameter> Parameters, Scope Scope) : Expression;