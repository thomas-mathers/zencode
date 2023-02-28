using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Abstractions.Types.Strategies;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types;

public abstract class BaseTypeParser : ITypeParser
{
    protected IReadOnlyDictionary<TokenType, IPrefixTypeParsingStrategy> PrefixStrategies { get; set; } =
        new Dictionary<TokenType, IPrefixTypeParsingStrategy>();
    
    protected IReadOnlyDictionary<TokenType, IInfixTypeParsingStrategy> InfixStrategies { get; set; } =
        new Dictionary<TokenType, IInfixTypeParsingStrategy>();
    
    public Type Parse(ITokenStream tokenStream, int precedence = 0)
    {
        var type = ParsePrefix(tokenStream);

        while (precedence < GetPrecedence(tokenStream))
        {
            type = ParseInfix(tokenStream, type);
        }
        
        return type;
    }

    private Type ParsePrefix(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!PrefixStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
    
    private Type ParseInfix(ITokenStream tokenStream, Type type)
    {
        var operatorToken = tokenStream.Current;

        if (!InfixStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return infixExpressionParsingStrategy.Parse(tokenStream, type);
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