using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    private readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> _prefixStrategies;
    private readonly IReadOnlyDictionary<TokenType, IInfixExpressionParsingStrategy> _infixStrategies;

    public ExpressionParser()
    {
        var expressionListParser = new ExpressionListParser(this);
        
        _prefixStrategies = new Dictionary<TokenType, IPrefixExpressionParsingStrategy>
        {
            [TokenType.Boolean] = new ConstantParsingStrategy(),
            [TokenType.Integer] = new ConstantParsingStrategy(),
            [TokenType.Float] = new ConstantParsingStrategy(),
            [TokenType.String] = new ConstantParsingStrategy(),
            [TokenType.Identifier] = new VariableReferenceParsingStrategy(expressionListParser),
            [TokenType.Subtraction] = new UnaryExpressionParsingStrategy(this),
            [TokenType.Not] = new UnaryExpressionParsingStrategy(this),
            [TokenType.LeftParenthesis] = new ParenthesizedExpressionParsingStrategy(this)
        };
        
        _infixStrategies = new Dictionary<TokenType, IInfixExpressionParsingStrategy>
        {
            [TokenType.Addition] = new BinaryExpressionParsingStrategy(this, 4),
            [TokenType.Subtraction] = new BinaryExpressionParsingStrategy(this, 4),
            [TokenType.Multiplication] = new BinaryExpressionParsingStrategy(this, 5),
            [TokenType.Division] = new BinaryExpressionParsingStrategy(this, 5),
            [TokenType.Modulus] = new BinaryExpressionParsingStrategy(this, 5),
            [TokenType.Exponentiation] = new BinaryExpressionParsingStrategy(this, 6, true),
            [TokenType.LessThan] = new BinaryExpressionParsingStrategy(this, 3),
            [TokenType.LessThanOrEqual] = new BinaryExpressionParsingStrategy(this, 3),
            [TokenType.Equals] = new BinaryExpressionParsingStrategy(this, 3),
            [TokenType.NotEquals] = new BinaryExpressionParsingStrategy(this, 3),
            [TokenType.GreaterThan] = new BinaryExpressionParsingStrategy(this, 3),
            [TokenType.GreaterThanOrEqual] = new BinaryExpressionParsingStrategy(this, 3),
            [TokenType.And] = new BinaryExpressionParsingStrategy(this, 2),
            [TokenType.Or] = new BinaryExpressionParsingStrategy(this, 1),
            [TokenType.LeftParenthesis] = new FunctionCallParsingStrategy(expressionListParser, 7)
        };
    }

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