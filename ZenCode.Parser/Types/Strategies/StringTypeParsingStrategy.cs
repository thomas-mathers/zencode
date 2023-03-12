using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies;

public class StringTypeParsingStrategy : IStringTypeParsingStrategy
{
    public StringType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.String);
        return new StringType();
    }
}