using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class LiteralParsingStrategy : ILiteralParsingStrategy
{
    public LiteralExpression Parse(ITokenStream tokenStream, TokenType tokenType)
    {
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        return new LiteralExpression(tokenStream.Consume(tokenType));
    }
}
