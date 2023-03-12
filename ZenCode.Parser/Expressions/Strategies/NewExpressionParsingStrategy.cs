using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class NewExpressionParsingStrategy : INewExpressionParsingStrategy
{
    public NewExpression Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.New);
        var type = parser.ParseType(tokenStream);
        tokenStream.Consume(TokenType.LeftBracket);
        var expressionList = parser.ParseExpressionList(tokenStream);
        tokenStream.Consume(TokenType.RightBracket);
        return new NewExpression(type, expressionList);
    }
}