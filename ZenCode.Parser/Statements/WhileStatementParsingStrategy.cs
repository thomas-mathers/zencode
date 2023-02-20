using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class WhileStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IStatementParser _statementParser;
    private readonly IExpressionParser _expressionParser;

    public WhileStatementParsingStrategy(IStatementParser statementParser, IExpressionParser expressionParser)
    {
        _statementParser = statementParser;
        _expressionParser = expressionParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.While);

        var conditionExpression = _expressionParser.Parse(tokenStream);

        tokenStream.Consume(TokenType.LeftBrace);

        var statements = new List<Statement>();
        
        while (!tokenStream.Match(TokenType.RightBrace))
        {
            statements.Add(_statementParser.Parse(tokenStream));
        }

        return new WhileStatement(conditionExpression) { Statements = statements };
    }
}