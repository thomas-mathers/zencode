using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types;

public class TypeParser : ITypeParser
{
    private readonly IDictionary<TokenType, IInfixTypeParsingStrategy> _infixTypeParsingStrategies =
        new Dictionary<TokenType, IInfixTypeParsingStrategy>();

    private readonly IDictionary<TokenType, IPrefixTypeParsingStrategy> _prefixTypeParsingStrategies =
        new Dictionary<TokenType, IPrefixTypeParsingStrategy>();

    public Type ParseType(ITokenStream tokenStream, int precedence = 0)
    {
        var type = ParsePrefixType(tokenStream);

        while (precedence < GetTypePrecedence(tokenStream))
        {
            type = ParseInfixType(tokenStream, type);
        }

        return type;
    }

    public ParameterList ParseParameterList(ITokenStream tokenStream)
    {
        var parameters = new List<Parameter>();

        do
        {
            var identifier = tokenStream.Consume(TokenType.Identifier);

            tokenStream.Consume(TokenType.Colon);

            var type = ParseType(tokenStream);

            parameters.Add(new Parameter(identifier, type));
        } while (tokenStream.Match(TokenType.Comma));

        return new ParameterList { Parameters = parameters };
    }

    public void SetPrefixTypeParsingStrategy(TokenType tokenType, IPrefixTypeParsingStrategy parsingStrategy)
    {
        _prefixTypeParsingStrategies[tokenType] = parsingStrategy;
    }

    public void SetInfixTypeParsingStrategy(TokenType tokenType, IInfixTypeParsingStrategy parsingStrategy)
    {
        _infixTypeParsingStrategies[tokenType] = parsingStrategy;
    }

    private Type ParsePrefixType(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixTypeParsingStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }

    private Type ParseInfixType(ITokenStream tokenStream, Type type)
    {
        var operatorToken = tokenStream.Current;

        if (!_infixTypeParsingStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return infixExpressionParsingStrategy.Parse(tokenStream, type);
    }

    private int GetTypePrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !_infixTypeParsingStrategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
}