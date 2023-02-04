using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Prefix;

public class UnaryExpressionParser : IPrefixExpressionParser
{
    public Expression Parse(IParser parser, Token op)
    {
        var expression = parser.ParseExpression();

        return new UnaryExpression(op, expression);
    }
}