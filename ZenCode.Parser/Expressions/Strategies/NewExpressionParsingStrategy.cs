using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class NewExpressionParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly ITypeParser _typeParser;
    private readonly IExpressionParser _expressionParser;

    public NewExpressionParsingStrategy(ITypeParser typeParser, IExpressionParser expressionParser)
    {
        _typeParser = typeParser;
        _expressionParser = expressionParser;
    }
    
    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.New);
        var type = _typeParser.ParseType(tokenStream);
        tokenStream.Consume(TokenType.LeftBracket);
        var expressionList = _expressionParser.ParseExpressionList(tokenStream);
        tokenStream.Consume(TokenType.RightBracket);
        return new NewExpression(type, expressionList);
    }
}