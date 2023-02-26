using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    public IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> PrefixStrategies { get; set; } =
        new Dictionary<TokenType, IPrefixExpressionParsingStrategy>();

    public IReadOnlyDictionary<TokenType, IInfixExpressionParsingStrategy> InfixStrategies { get; set; } =
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
    
    private Expression ParsePrefix(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!PrefixStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
    
    private Expression ParseInfix(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Current;

        if (!InfixStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
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

        return !InfixStrategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
}