using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class UnaryExpressionParsingStrategy : IUnaryExpressionParsingStrategy
{
    public UnaryExpression Parse(IParser parser, ITokenStream tokenStream, TokenType operatorType)
    {
        var operatorToken = tokenStream.Consume(operatorType);

        var expression = parser.ParseExpression(tokenStream);

        return new UnaryExpression(operatorToken, expression);
    }
}