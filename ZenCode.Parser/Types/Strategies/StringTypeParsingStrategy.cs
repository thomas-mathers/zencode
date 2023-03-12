using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;

namespace ZenCode.Parser.Types.Strategies;

public class StringTypeParsingStrategy
{
    public StringType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.String);
        return new StringType();
    }
}