using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Prefix;

public class UnaryExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        var expression = parser.Parse(tokenStream);

        return new UnaryExpression(token, expression);
    }
}