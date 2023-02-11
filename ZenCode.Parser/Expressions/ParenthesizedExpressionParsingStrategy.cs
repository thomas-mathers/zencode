using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ParenthesizedExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        var innerExpression = parser.Parse(tokenStream);
        tokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}