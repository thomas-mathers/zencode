using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class ParenthesisParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public ParenthesisParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);
        var innerExpression = _expressionParser.ParseExpression(tokenStream);
        tokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}