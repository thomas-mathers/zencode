using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Prefix;

public class ParenthesizedExpressionParser : IPrefixExpressionParser
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        var innerExpression = parser.Parse(tokenStream);
        tokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}