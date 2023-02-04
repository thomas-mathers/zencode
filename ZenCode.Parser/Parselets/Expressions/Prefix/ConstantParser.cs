using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Prefix;

public class ConstantParser : IPrefixExpressionParser
{
    public Expression Parse(IParser parser, Token literal)
    {
        return new ConstantExpression(literal);
    }
}