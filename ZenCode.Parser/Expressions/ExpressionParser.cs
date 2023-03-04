using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IDictionary<TokenType, IInfixExpressionParsingStrategy> _infixExpressionParsingStrategies =
        new Dictionary<TokenType, IInfixExpressionParsingStrategy>();

    private readonly IDictionary<TokenType, IPrefixExpressionParsingStrategy> _prefixExpressionParsingStrategies =
        new Dictionary<TokenType, IPrefixExpressionParsingStrategy>();

    public Expression ParseExpression(ITokenStream tokenStream, int precedence = 0)
    {
        var lExpression = ParsePrefixExpression(tokenStream);

        while (precedence < GetExpressionPrecedence(tokenStream))
        {
            lExpression = ParseInfixExpression(tokenStream, lExpression);
        }

        return lExpression;
    }

    public ExpressionList ParseExpressionList(ITokenStream tokenStream)
    {
        var expressions = new List<Expression>();

        do
        {
            expressions.Add(ParseExpression(tokenStream));
        } while (tokenStream.Match(TokenType.Comma));

        return new ExpressionList { Expressions = expressions };
    }

    public void SetPrefixExpressionParsingStrategy(TokenType tokenType,
        IPrefixExpressionParsingStrategy parsingStrategy)
    {
        _prefixExpressionParsingStrategies[tokenType] = parsingStrategy;
    }

    public void SetInfixExpressionParsingStrategy(TokenType tokenType, IInfixExpressionParsingStrategy parsingStrategy)
    {
        _infixExpressionParsingStrategies[tokenType] = parsingStrategy;
    }

    private Expression ParsePrefixExpression(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixExpressionParsingStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }

    private Expression ParseInfixExpression(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Current;

        if (!_infixExpressionParsingStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return infixExpressionParsingStrategy.Parse(tokenStream, lOperand);
    }

    private int GetExpressionPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !_infixExpressionParsingStrategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
}