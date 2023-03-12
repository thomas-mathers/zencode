using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;

namespace ZenCode.Parser.Types.Strategies;

public class FloatTypeParsingStrategy
{
    public FloatType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Float);
        return new FloatType();
    }
}