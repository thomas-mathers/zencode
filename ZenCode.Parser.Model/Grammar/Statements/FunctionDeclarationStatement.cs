using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record FunctionDeclarationStatement(Type ReturnType, Token Identifier, ParameterList Parameters, Scope Scope) : CompoundStatement
{
    public override string ToString()
    {
        return $"function {Identifier}({Parameters}) => {ReturnType} {Scope}";
    }
}