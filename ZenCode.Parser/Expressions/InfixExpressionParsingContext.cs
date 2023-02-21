using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class InfixExpressionParsingContext : IInfixExpressionParsingContext
{
    public required IReadOnlyDictionary<TokenType, IInfixExpressionParsingStrategy> Strategies { get; init; }
    
    public Expression Parse(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Current;

        if (!Strategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return infixExpressionParsingStrategy.Parse(tokenStream, lOperand);
    }

    public int GetPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !Strategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
}