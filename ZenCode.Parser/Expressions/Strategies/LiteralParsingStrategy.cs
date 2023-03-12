using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class LiteralParsingStrategy : ILiteralParsingStrategy
{
    public LiteralExpression Parse(ITokenStream tokenStream, TokenType tokenType)
    {
        return new LiteralExpression(tokenStream.Consume(tokenType));
    }
}