using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class UnaryExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IExpressionParser _parser;

    public UnaryExpressionParsingStrategy(IExpressionParser parser)
    {
        _parser = parser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        var operatorToken = tokenStream.Consume();

        var expression = _parser.Parse(tokenStream);

        return new UnaryExpression(operatorToken, expression);
    }
}