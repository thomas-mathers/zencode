using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class ParenthesisParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IParser _parser;

    public ParenthesisParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);
        var innerExpression = _parser.ParseExpression(tokenStream);
        tokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}