using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class ParenthesisParsingStrategy
{
    public Expression Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);
        var innerExpression = parser.ParseExpression(tokenStream);
        tokenStream.Consume(TokenType.RightParenthesis);
        return innerExpression;
    }
}