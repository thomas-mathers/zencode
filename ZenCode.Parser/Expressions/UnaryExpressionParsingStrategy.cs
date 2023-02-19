using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class UnaryExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _parser;

    public UnaryExpressionParsingStrategy(IExpressionParser parser)
    {
        _parser = parser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        var operatorToken = tokenStream.Consume();

        if (!IsUnaryOperator(operatorToken.Type))
        {
            throw new UnexpectedTokenException();
        }

        var expression = _parser.Parse(tokenStream);

        return new UnaryExpression(operatorToken, expression);
    }

    private static bool IsUnaryOperator(TokenType tokenType) =>
        tokenType switch
        {
            TokenType.Not => true,
            TokenType.Subtraction => true,
            _ => false
        };
}