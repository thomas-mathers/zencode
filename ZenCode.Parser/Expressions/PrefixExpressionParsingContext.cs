using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class PrefixExpressionParsingContext : IPrefixExpressionParsingContext
{
    public required IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> Strategies { get; init; }
    
    public Expression Parse(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!Strategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
}