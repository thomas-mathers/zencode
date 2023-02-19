using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class IfStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;
    private readonly IStatementParser _statementParser;

    public IfStatementParsingStrategy(IStatementParser statementParser, IExpressionParser expressionParser)
    {
        _statementParser = statementParser;
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
            statements.Add(_statementParser.Parse(tokenStream));
        }
        
        tokenStream.Consume(TokenType.RightBrace);
        
        return new IfStatement(conditionExpression, statements);
    }
}