using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Types.Strategies;

public class BooleanTypeParsingStrategy : IBooleanTypeParsingStrategy
{
    public BooleanType Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Boolean);
        
        return new BooleanType();
    }
}