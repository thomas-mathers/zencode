using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Infix;

public class BinaryExpressionParser : IInfixExpressionParser
{
    private readonly int _precedence;
    private readonly bool _isRightAssociative;
    
    public BinaryExpressionParser(int precedence, bool isRightAssociative = false)
    {
        _precedence = precedence;
        _isRightAssociative = isRightAssociative;
    }

    public Expression Parse(IParser parser, Expression lOperand, Token @operator)
    {
        var rOperand = parser.ParseExpression(_precedence - (_isRightAssociative ? 1 : 0));

        return new BinaryExpression(lOperand, @operator, rOperand);
    }

    public int GetPrecedence()
    {
        return _precedence;
    }
}