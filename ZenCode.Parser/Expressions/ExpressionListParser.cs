using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionListParser : IExpressionListParser
{
    private readonly IExpressionParser _expressionParser;

    public ExpressionListParser(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }
    
    public IReadOnlyList<Expression> Parse(ITokenStream tokenStream)
    {
        var expressions = new List<Expression>();
        
        do
        {
            expressions.Add(_expressionParser.Parse(tokenStream));
        } while (tokenStream.Match(TokenType.Comma));

        return expressions;
    }
}