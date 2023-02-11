using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;

namespace ZenCode.Parser.Expressions;

public class PrefixExpressionParsingContext : IPrefixExpressionParsingContext
{
    private static readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> PrefixExpressionParsers =
        new Dictionary<TokenType, IPrefixExpressionParsingStrategy>
        {
            [TokenType.Boolean] = new ConstantParsingStrategy(),
            [TokenType.Integer] = new ConstantParsingStrategy(),
            [TokenType.Float] = new ConstantParsingStrategy(),
            [TokenType.Identifier] = new VariableReferenceParsingStrategy(),
            [TokenType.Not] = new UnaryExpressionParsingStrategy(),
            [TokenType.LeftParenthesis] = new ParenthesizedExpressionParsingStrategy()
        };

    public Expression Parse(IExpressionParser parser, ITokenStream tokenStream, Token token)
    {
        if (!PrefixExpressionParsers.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
            throw new ParseException();

        return prefixExpressionParsingStrategy.Parse(parser, tokenStream, token);
    }
}