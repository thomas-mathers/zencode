using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record AnonymousFunctionDeclarationExpression
    (Type ReturnType, ParameterList Parameters, Scope Scope) : Expression
{
    public override string ToString()
    {
        return $"function ({Parameters}) => {ReturnType} {Scope}";
    }
}