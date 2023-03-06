using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ReturnStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public ReturnStatementParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Return);
        
        if (tokenStream.Match(TokenType.Semicolon))
        {
            return new ReturnStatement();
        }
        
        var expression = _expressionParser.ParseExpression(tokenStream);
        
        tokenStream.Consume(TokenType.Semicolon);
        
        return new ReturnStatement { Expression = expression };
    }
}