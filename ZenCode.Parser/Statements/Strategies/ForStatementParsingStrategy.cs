using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ForStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;
    private readonly IStatementParser _statementParser;

    public ForStatementParsingStrategy(IExpressionParser expressionParser, IStatementParser statementParser)
    {
        _expressionParser = expressionParser;
        _statementParser = statementParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.For);

        var initializer = _statementParser.ParseStatement(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        var condition = _expressionParser.ParseExpression(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        var iterator = _statementParser.ParseStatement(tokenStream);

        var scope = _statementParser.ParseScope(tokenStream);
        
        return new ForStatement(initializer, condition, iterator, scope);
    }
}