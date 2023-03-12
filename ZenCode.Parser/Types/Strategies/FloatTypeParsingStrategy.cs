using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies;

public class FloatTypeParsingStrategy : IFloatTypeParsingStrategy
{
    public FloatType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Float);
        return new FloatType();
    }
}