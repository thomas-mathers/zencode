using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class NewArrayExpressionParsingStrategy : INewExpressionParsingStrategy
{
    public NewArrayExpression Parse(IParser parser, ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.New);

        var type = parser.ParseType(tokenStream);
        tokenStream.Consume(TokenType.LeftBracket);

        var size = parser.ParseExpression(tokenStream);
        tokenStream.Consume(TokenType.RightBracket);

        return new NewArrayExpression
        {
            Type = type,
            Size = size
        };
    }
}
