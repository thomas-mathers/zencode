using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class UnaryExpressionParsingStrategy : IUnaryExpressionParsingStrategy
{
    public UnaryExpression Parse(IParser parser, ITokenStream tokenStream)
    {
        var operatorToken = tokenStream.Consume();

        if (!IsUnaryOperator(operatorToken.Type))
        {
            throw new UnexpectedTokenException();
        }

        var expression = parser.ParseExpression(tokenStream);

        return new UnaryExpression(operatorToken, expression);
    }

    private static bool IsUnaryOperator(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Not => true,
            TokenType.Minus => true,
            _ => false
        };
    }
}