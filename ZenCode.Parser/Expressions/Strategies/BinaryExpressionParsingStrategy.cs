using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class BinaryExpressionParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly IParser _parser;

    public BinaryExpressionParsingStrategy(IParser parser, int precedence, bool isRightAssociative = false)
    {
        _parser = parser;
        Precedence = precedence;
        IsRightAssociative = isRightAssociative;
    }

    private bool IsRightAssociative { get; }

    public int Precedence { get; }

    public Expression Parse(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Consume();

        if (!IsBinaryOperator(operatorToken.Type))
        {
            throw new UnexpectedTokenException();
        }

        var rOperand = _parser.ParseExpression(tokenStream, IsRightAssociative ? Precedence - 1 : Precedence);

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
            _ => false
        };
    }
}