using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IInfixExpressionParsingContext _infixExpressionParsingContext;
    private readonly IPrefixExpressionParsingContext _prefixExpressionParsingContext;

    public ExpressionParser(IPrefixExpressionParsingContext prefixExpressionParsingContext,
        IInfixExpressionParsingContext infixExpressionParsingContext)

    {
        _prefixExpressionParsingContext = prefixExpressionParsingContext;
        _infixExpressionParsingContext = infixExpressionParsingContext;
    }

    public Expression Parse(ITokenStream tokenStream, int precedence = 0)
    {
        var lExpression = _prefixExpressionParsingContext.Parse(this, tokenStream, tokenStream.Consume());

        while (precedence < _infixExpressionParsingContext.GetPrecedence(tokenStream))
            lExpression = _infixExpressionParsingContext.Parse(this, tokenStream, lExpression, tokenStream.Consume());

        return lExpression;
    }
}