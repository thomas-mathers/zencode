using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IInfixExpressionParser _infixExpressionParser;
    private readonly IPrefixExpressionParser _prefixExpressionParser;

    public ExpressionParser(
        IPrefixExpressionParser prefixExpressionParser,
        IInfixExpressionParser infixExpressionParser)
    {
        _prefixExpressionParser = prefixExpressionParser;
        _infixExpressionParser = infixExpressionParser;
    }

    public Expression ParseExpression(IParser parser, ITokenStream tokenStream, int precedence = 0)
    {
        var lExpression = _prefixExpressionParser.ParsePrefixExpression(parser, tokenStream);

        while (precedence < InfixExpressionParser.GetPrecedence(tokenStream.Peek(0)?.Type))
            lExpression = _infixExpressionParser.ParseInfixExpression(parser, tokenStream, lExpression);

        return lExpression;
    }

    public VariableReferenceExpression ParseVariableReferenceExpression(IParser parser, ITokenStream tokenStream)
    {
        return _prefixExpressionParser.ParseVariableReferenceExpression(parser, tokenStream);
    }
}