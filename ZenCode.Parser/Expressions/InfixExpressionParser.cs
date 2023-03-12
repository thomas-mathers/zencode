using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class InfixExpressionParser : IInfixExpressionParser
{
    private readonly BinaryExpressionParsingStrategy _binaryExpressionParsingStrategy;
    private readonly FunctionCallParsingStrategy _functionCallParsingStrategy;
    
    private static readonly IReadOnlyDictionary<TokenType, int> OperatorPrecedences = new Dictionary<TokenType, int>
    {
        [TokenType.Or] = 1,
        [TokenType.And] = 2,
        [TokenType.LessThan] = 3,
        [TokenType.LessThanOrEqual] = 3,
        [TokenType.Equals] = 3,
        [TokenType.NotEquals] = 3,
        [TokenType.GreaterThan] = 3,
        [TokenType.GreaterThanOrEqual] = 3,
        [TokenType.Plus] = 4,
        [TokenType.Minus] = 4,
        [TokenType.Multiplication] = 5,
        [TokenType.Division] = 5,
        [TokenType.Modulus] = 5,
        [TokenType.Exponentiation] = 6,
        [TokenType.LeftParenthesis] = 7,
    };

    public InfixExpressionParser(BinaryExpressionParsingStrategy binaryExpressionParsingStrategy,
        FunctionCallParsingStrategy functionCallParsingStrategy)
    {
        _binaryExpressionParsingStrategy = binaryExpressionParsingStrategy;
        _functionCallParsingStrategy = functionCallParsingStrategy;
    }
    
    public Expression ParseInfixExpression(IParser parser, ITokenStream tokenStream, Expression lOperand)
    {
        switch (tokenStream.Current.Type)
        {
            case TokenType.Plus:
            case TokenType.Minus:
            case TokenType.Multiplication:
            case TokenType.Division:
            case TokenType.Modulus:
            case TokenType.Exponentiation:
            case TokenType.LessThan:
            case TokenType.LessThanOrEqual:
            case TokenType.Equals:
            case TokenType.NotEquals:
            case TokenType.GreaterThan:
            case TokenType.GreaterThanOrEqual:
            case TokenType.And:
            case TokenType.Or:
                return ParseBinaryExpression(parser, tokenStream, lOperand);
            case TokenType.LeftParenthesis:
                return ParseFunctionCallExpression(parser, tokenStream, lOperand);
            default:
                throw new UnexpectedTokenException();
        }
    }

    public FunctionCallExpression ParseFunctionCallExpression(IParser parser, ITokenStream tokenStream, Expression lOperand)
    {
        return _functionCallParsingStrategy.Parse(parser, tokenStream, lOperand);
    }

    public BinaryExpression ParseBinaryExpression(IParser parser, ITokenStream tokenStream, Expression lOperand)
    {
        return _binaryExpressionParsingStrategy.Parse(parser, tokenStream, lOperand, tokenStream.Current.Type, GetPrecedence(tokenStream.Current.Type), IsRightAssociative(tokenStream.Current.Type));
    }

    public static int GetPrecedence(TokenType? tokenType)
    {
        if (tokenType == null)
        {
            return 0;
        }

        return !OperatorPrecedences.TryGetValue(tokenType.Value, out var precedence)
            ? 0
            : precedence;
    }

    private static bool IsRightAssociative(TokenType tokenType) =>
        tokenType switch
        {
            TokenType.Exponentiation => true,
            _ => false
        };
}