using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Expressions;
using ZenCode.Parser.Parsers.Expressions.Infix;
using ZenCode.Parser.Parsers.Expressions.Prefix;

namespace ZenCode.Parser.Parsers.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IPrefixExpressionParsingContext _prefixExpressionParsingContext;
    private readonly IInfixExpressionParsingContext _infixExpressionParsingContext;

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
        {
            lExpression = _infixExpressionParsingContext.Parse(this, tokenStream, lExpression, tokenStream.Consume());   
        }

        return lExpression;
    }
}