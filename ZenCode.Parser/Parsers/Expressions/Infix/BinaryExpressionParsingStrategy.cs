using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Infix;

public class BinaryExpressionParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly int _precedence;
    private readonly bool _isRightAssociative;
    
    public BinaryExpressionParsingStrategy(int precedence, bool isRightAssociative = false)
    {
        _precedence = precedence;
        _isRightAssociative = isRightAssociative;
    }

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator)
    {
        var rOperand = parser.Parse(tokenStream, _precedence - (_isRightAssociative ? 1 : 0));

        return new BinaryExpression(lOperand, @operator, rOperand);
    }

    public int GetPrecedence()
    {
        return _precedence;
    }
}