using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Abstractions.Types.Strategies;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types;

public class TypeParser : ITypeParser
{
    private readonly IDictionary<TokenType, IPrefixTypeParsingStrategy> _prefixStrategies =
        new Dictionary<TokenType, IPrefixTypeParsingStrategy>();
    
    private readonly IDictionary<TokenType, IInfixTypeParsingStrategy> _infixStrategies =
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
    
    public void SetPrefixParsingStrategy(TokenType tokenType, IPrefixTypeParsingStrategy parsingStrategy)
    {
        _prefixStrategies[tokenType] = parsingStrategy;
    }
    
    public void SetInfixParsingStrategy(TokenType tokenType, IInfixTypeParsingStrategy parsingStrategy)
    {
        _infixStrategies[tokenType] = parsingStrategy;
    }

    private Type ParsePrefix(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
    
    private Type ParseInfix(ITokenStream tokenStream, Type type)
    {
        var operatorToken = tokenStream.Current;

        if (!_infixStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
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

        return !_infixStrategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
}