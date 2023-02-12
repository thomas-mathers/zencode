using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class PrefixExpressionParsingContext : IPrefixExpressionParsingContext
{
    private readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> _prefixExpressionParsingStrategies;

    public PrefixExpressionParsingContext(IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> prefixExpressionParsingStrategies)
    {
        _prefixExpressionParsingStrategies = prefixExpressionParsingStrategies;
    }

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        if (!_prefixExpressionParsingStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
            throw new SyntaxError();

        return prefixExpressionParsingStrategy.Parse(parser, tokenStream, token);
    }
}