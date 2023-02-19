using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class IfStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public IfStatementParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.If);
        
        var conditionExpression = _expressionParser.Parse(tokenStream);
        
        tokenStream.Consume(TokenType.LeftBrace);
        
        var statements = new List<Statement>();
        
        while (tokenStream.Current.Type != TokenType.RightBrace)
        {
            statements.Add(_expressionParser.Parse(tokenStream));
        }
        
        tokenStream.Consume(TokenType.RightBrace);
        
        return new IfStatement(conditionExpression, statements);
    }
}