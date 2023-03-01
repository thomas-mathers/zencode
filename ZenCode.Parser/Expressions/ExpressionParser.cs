using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IDictionary<TokenType, IPrefixExpressionParsingStrategy> _prefixStrategies =
        new Dictionary<TokenType, IPrefixExpressionParsingStrategy>();

    private readonly IDictionary<TokenType, IInfixExpressionParsingStrategy> _infixStrategies =
        new Dictionary<TokenType, IInfixExpressionParsingStrategy>();

    public Expression Parse(ITokenStream tokenStream, int precedence = 0)
    {
        var lExpression = ParsePrefix(tokenStream);

        while (precedence < GetPrecedence(tokenStream))
        {
            lExpression = ParseInfix(tokenStream, lExpression);
        }

        return lExpression;
    }
    
    public IReadOnlyList<Expression> ParseList(ITokenStream tokenStream)
    {
        var expressions = new List<Expression>();
        
        do
        {
            expressions.Add(Parse(tokenStream));
        } while (tokenStream.Match(TokenType.Comma));

        return expressions;
    }
    
    public void SetPrefixStrategy(TokenType tokenType, IPrefixExpressionParsingStrategy parsingStrategy)
    {
        _prefixStrategies[tokenType] = parsingStrategy;
    }
    
    public void SetInfixStrategy(TokenType tokenType, IInfixExpressionParsingStrategy parsingStrategy)
    {
        _infixStrategies[tokenType] = parsingStrategy;
    }
    
    private Expression ParsePrefix(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
    
    private Expression ParseInfix(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Current;

        if (!_infixStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return infixExpressionParsingStrategy.Parse(tokenStream, lOperand);
    }

    private int GetPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !_infixStrategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
}