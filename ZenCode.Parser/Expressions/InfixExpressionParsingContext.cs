using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class InfixExpressionParsingContext : IInfixExpressionParsingContext
{
    private static readonly IReadOnlyDictionary<TokenType, IInfixExpressionParsingStrategy> InfixExpressionParsers = new Dictionary<TokenType, IInfixExpressionParsingStrategy>()
    {
        [TokenType.Addition] = new BinaryExpressionParsingStrategy(4),
        [TokenType.Subtraction] = new BinaryExpressionParsingStrategy(4),
        [TokenType.Multiplication] = new BinaryExpressionParsingStrategy(5),
        [TokenType.Division] = new BinaryExpressionParsingStrategy(5),
        [TokenType.Modulus] = new BinaryExpressionParsingStrategy(5),
        [TokenType.Exponentiation] = new BinaryExpressionParsingStrategy(6, true),
        [TokenType.LessThan] = new BinaryExpressionParsingStrategy(3),
        [TokenType.LessThanOrEqual] = new BinaryExpressionParsingStrategy(3),
        [TokenType.Equals] = new BinaryExpressionParsingStrategy(3),
        [TokenType.NotEquals] = new BinaryExpressionParsingStrategy(3),
        [TokenType.GreaterThan] = new BinaryExpressionParsingStrategy(3),
        [TokenType.GreaterThanOrEqual] = new BinaryExpressionParsingStrategy(3),
        [TokenType.And] = new BinaryExpressionParsingStrategy(2),
        [TokenType.Or] = new BinaryExpressionParsingStrategy(1),
        [TokenType.LeftParenthesis] = new FunctionCallParsingStrategy(7)
    };
    
    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator)
    {
        if (!InfixExpressionParsers.TryGetValue(@operator.Type, out var infixExpressionParsingStrategy))
        {
            throw new ParseException();   
        }

        return infixExpressionParsingStrategy.Parse(parser, tokenStream, lOperand, @operator);
    }

    public int GetPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream?.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !InfixExpressionParsers.TryGetValue(currentToken.Type, out var parser) 
            ? 0 
            : parser.GetPrecedence();
    }
}