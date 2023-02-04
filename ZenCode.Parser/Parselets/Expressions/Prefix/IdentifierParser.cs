using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Prefix;

public class IdentifierParser : IPrefixExpressionParser
{
    public Expression Parse(IParser parser, Token identifier)
    {
        return new IdentifierExpression(identifier);
    }
}