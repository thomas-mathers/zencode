using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Infix;

public class BinaryExpressionParser : IInfixExpressionParser
{
    public Expression Parse(IParser parser, Expression lOperand, Token @operator)
    {
        var rOperand = parser.ParseExpression();

        return new BinaryExpression(lOperand, @operator, rOperand);
    }
}