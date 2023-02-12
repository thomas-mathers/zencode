using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class InfixExpressionParsingContext : IInfixExpressionParsingContext
{
    private readonly IReadOnlyDictionary<TokenType, IInfixExpressionParsingStrategy> _infixExpressionParsingStrategies;

    public InfixExpressionParsingContext(IReadOnlyDictionary<TokenType, IInfixExpressionParsingStrategy> infixExpressionParsingStrategies)
    {
        _infixExpressionParsingStrategies = infixExpressionParsingStrategies;
    }

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Expression lOperand, Token @operator)
    {
        if (!_infixExpressionParsingStrategies.TryGetValue(@operator.Type, out var infixExpressionParsingStrategy))
            throw new SyntaxError();

        return infixExpressionParsingStrategy.Parse(parser, tokenStream, lOperand, @operator);
    }

    public int GetPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream?.Peek(0);

        if (currentToken == null)
            return 0;

        return !_infixExpressionParsingStrategies.TryGetValue(currentToken.Type, out var parser)
            ? 0
            : parser.GetPrecedence();
    }
}