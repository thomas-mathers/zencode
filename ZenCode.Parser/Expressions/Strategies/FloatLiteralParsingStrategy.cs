using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class FloatLiteralParsingStrategy
{
    public LiteralExpression Parse(ITokenStream tokenStream)
    {
        return new LiteralExpression(tokenStream.Consume(TokenType.FloatLiteral));
    }
}