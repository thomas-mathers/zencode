using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;

namespace ZenCode.Parser.Types.Strategies;

public class BooleanTypeParsingStrategy
{
    public BooleanType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Boolean);
        return new BooleanType();
    }
}