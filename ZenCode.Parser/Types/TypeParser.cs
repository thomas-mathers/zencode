using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Types;

public class TypeParser : ITypeParser
{
    private readonly IDictionary<TokenType, IPrefixTypeParsingStrategy> _prefixTypeParsingStrategies =
        new Dictionary<TokenType, IPrefixTypeParsingStrategy>();

    public Type ParseType(ITokenStream tokenStream, int precedence = 0)
    {
        var type = ParsePrefixType(tokenStream);

        while (tokenStream.Peek(0)?.Type == TokenType.LeftBracket && tokenStream.Peek(1)?.Type == TokenType.RightBracket)
        {
            tokenStream.Consume(TokenType.LeftBracket);
            tokenStream.Consume(TokenType.RightBracket);
            
            type = new ArrayType(type);
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

    private Type ParsePrefixType(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixTypeParsingStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
}