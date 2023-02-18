using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ParenthesizedExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _parser;

    public ParenthesizedExpressionParsingStrategy(IExpressionParser parser)
    {
        _parser = parser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);
        var innerExpression = _parser.Parse(tokenStream);
        tokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}