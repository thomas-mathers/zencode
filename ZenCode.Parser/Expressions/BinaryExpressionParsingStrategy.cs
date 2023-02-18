using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class BinaryExpressionParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly bool _isRightAssociative;
    private readonly IExpressionParser _parser;

    public BinaryExpressionParsingStrategy(IExpressionParser parser, int precedence, bool isRightAssociative = false)
    {
        _parser = parser;
        Precedence = precedence;
        _isRightAssociative = isRightAssociative;
    }

    public int Precedence { get; }

    public Expression Parse(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Consume();

        if (!IsBinaryOperator(operatorToken.Type))
        {
            throw new UnexpectedTokenException();
        }

        var rOperand = _parser.Parse(tokenStream, _isRightAssociative ? Precedence - 1 : Precedence);

        return new BinaryExpression(lOperand, operatorToken, rOperand);
    }

    private static bool IsBinaryOperator(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Addition => true,
            TokenType.Subtraction => true,
            TokenType.Multiplication => true,
            TokenType.Division => true,
            TokenType.Modulus => true,
            TokenType.Exponentiation => true,
            TokenType.LessThan => true,
            TokenType.LessThanOrEqual => true,
            TokenType.Equals => true,
            TokenType.NotEquals => true,
            TokenType.GreaterThan => true,
            TokenType.GreaterThanOrEqual => true,
            TokenType.And => true,
            TokenType.Or => true,
            TokenType.Not => true,
            _ => false
        };
    }
}