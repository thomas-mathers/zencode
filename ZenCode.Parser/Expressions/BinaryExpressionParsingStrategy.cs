using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

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