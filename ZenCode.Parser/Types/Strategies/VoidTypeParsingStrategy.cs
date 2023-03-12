using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;

namespace ZenCode.Parser.Types.Strategies;

public class VoidTypeParsingStrategy
{
    public VoidType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Void);
        return new VoidType();
    }
}