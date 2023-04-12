using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar;

public record Parameter(Token Identifier, Type Type) : AstNode
{
    public override string ToString()
    {
        return $"{Identifier} : {Type}";
    }
}
