using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class ParenthesisParsingStrategy : IParenthesisParsingStrategy
{
    public Expression Parse(IParser parser, ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.LeftParenthesis);

        var innerExpression = parser.ParseExpression(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        return innerExpression;
    }
}
