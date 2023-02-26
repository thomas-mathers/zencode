using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class ArgumentListParser : IArgumentListParser
{
    private readonly IExpressionParser _expressionParser;

    public ArgumentListParser(IExpressionParser expressionParser)
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