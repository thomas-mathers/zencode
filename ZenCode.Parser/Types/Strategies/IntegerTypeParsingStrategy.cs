using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types.Strategies;

public class IntegerTypeParsingStrategy : IPrefixTypeParsingStrategy
{
    public Type Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Integer);
        return new IntegerType();
    }
}