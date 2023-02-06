using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Infix;

public class BinaryExpressionParser : IInfixExpressionParser
{
    private readonly int _precedence;
    
    public BinaryExpressionParser(int precedence)
    {
        _precedence = precedence;
    }

    public Expression Parse(IParser parser, Expression lOperand, Token @operator)
    {
        var rOperand = parser.ParseExpression(_precedence);

        return new BinaryExpression(lOperand, @operator, rOperand);
    }

    public int GetPrecedence()
    {
        return _precedence;
    }
}