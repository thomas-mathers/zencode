using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class UnaryExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public UnaryExpressionParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        var operatorToken = tokenStream.Consume();

        if (!IsUnaryOperator(operatorToken.Type))
        {
            throw new UnexpectedTokenException();
        }

        var expression = _expressionParser.ParseExpression(tokenStream);

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