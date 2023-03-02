using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class UnaryExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IParser _parser;

    public UnaryExpressionParsingStrategy(IParser parser)
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

        var expression = _parser.ParseExpression(tokenStream);

        return new UnaryExpression(operatorToken, expression);
    }

    private static bool IsUnaryOperator(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Not => true,
            TokenType.Subtraction => true,
            _ => false
        };
    }
}