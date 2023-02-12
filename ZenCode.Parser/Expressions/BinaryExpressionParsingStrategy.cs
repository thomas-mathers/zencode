using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class BinaryExpressionParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly bool _isRightAssociative;
    private readonly int _precedence;

    public BinaryExpressionParsingStrategy(int precedence, bool isRightAssociative = false)
    {
        _precedence = precedence;
        _isRightAssociative = isRightAssociative;
    }

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator)
    {
        if (!IsOperatorValid(@operator.Type))
        {
            throw new SyntaxError();
        }
        
        var rOperand = parser.Parse(tokenStream, _precedence - (_isRightAssociative ? 1 : 0));

        return new BinaryExpression(lOperand, @operator, rOperand);
    }

    private bool IsOperatorValid(TokenType tokenType) =>
        tokenType switch
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

    public int GetPrecedence() => _precedence;
}