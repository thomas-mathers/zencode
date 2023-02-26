using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class ParenthesisParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _parser;

    public ParenthesisParsingStrategy(IExpressionParser parser)
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