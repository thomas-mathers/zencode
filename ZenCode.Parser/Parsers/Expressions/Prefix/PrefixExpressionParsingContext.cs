using ZenCode.Lexer;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser.Parsers.Expressions.Prefix;

public class PrefixExpressionParsingContext : IPrefixExpressionParsingContext
{
    private static readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParsingStrategy> PrefixExpressionParsers = new Dictionary<TokenType, IPrefixExpressionParsingStrategy>()
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
        {
            throw new ParseException();   
        }

        return prefixExpressionParsingStrategy.Parse(parser, tokenStream, token);
    }
}