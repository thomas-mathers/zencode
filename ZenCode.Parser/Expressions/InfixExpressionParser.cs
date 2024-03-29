using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class InfixExpressionParser : IInfixExpressionParser
{
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
        [TokenType.LeftParenthesis] = 7
    };

    private readonly IBinaryExpressionParsingStrategy _binaryExpressionParsingStrategy;
    private readonly IFunctionCallParsingStrategy _functionCallParsingStrategy;

    public InfixExpressionParser
    (
        IBinaryExpressionParsingStrategy binaryExpressionParsingStrategy,
        IFunctionCallParsingStrategy functionCallParsingStrategy
    )
    {
        _binaryExpressionParsingStrategy = binaryExpressionParsingStrategy;
        _functionCallParsingStrategy = functionCallParsingStrategy;
    }

    public Expression ParseInfixExpression(IParser parser, ITokenStream tokenStream, Expression lOperand)
    {
        if (tokenStream.Current == null)
        {
            throw new EndOfTokenStreamException();
        }
        
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
                throw new UnexpectedTokenException(tokenStream.Current);
        }
    }

    public static int GetPrecedence(TokenType? tokenType)
    {
        return tokenType != null && OperatorPrecedences.TryGetValue(tokenType.Value, out var precedence)
            ? precedence
            : 0;
    }

    private FunctionCallExpression ParseFunctionCallExpression
    (
        IParser parser,
        ITokenStream tokenStream,
        Expression lOperand
    )
    {
        return _functionCallParsingStrategy.Parse(parser, tokenStream, lOperand);
    }

    private BinaryExpression ParseBinaryExpression(IParser parser, ITokenStream tokenStream, Expression lOperand)
    {
        return _binaryExpressionParsingStrategy.Parse
        (
            parser,
            tokenStream,
            lOperand,
            tokenStream.Current.Type,
            GetPrecedence(tokenStream.Current.Type),
            IsRightAssociative(tokenStream.Current.Type)
        );
    }

    private static bool IsRightAssociative(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Exponentiation => true,
            _ => false
        };
    }
}
