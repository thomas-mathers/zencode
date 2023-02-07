using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parselets.Expressions.Prefix;

public class ParenthesizedExpressionParser : IPrefixExpressionParser
{
    public Expression Parse(IParser parser, Token token)
    {
        var innerExpression = parser.ParseExpression();
        parser.TokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}