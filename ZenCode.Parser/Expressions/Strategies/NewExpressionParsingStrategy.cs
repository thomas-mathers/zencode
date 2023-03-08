using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class NewExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IParser _parser;

    public NewExpressionParsingStrategy(IParser parser)
    {
        _parser = parser;
    }
    
    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.New);
        var type = _parser.ParseType(tokenStream);
        tokenStream.Consume(TokenType.LeftBracket);
        var expressionList = _parser.ParseExpressionList(tokenStream);
        tokenStream.Consume(TokenType.RightBracket);
        return new NewExpression(type, expressionList);
    }
}