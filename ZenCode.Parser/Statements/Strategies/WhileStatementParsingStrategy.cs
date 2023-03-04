using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class WhileStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;
    private readonly IStatementParser _statementParser;

    public WhileStatementParsingStrategy(IExpressionParser expressionParser, IStatementParser statementParser)
    {
        _expressionParser = expressionParser;
        _statementParser = statementParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.While);

        var condition = _expressionParser.ParseExpression(tokenStream);
        var scope = _statementParser.ParseScope(tokenStream);
        var conditionScope = new ConditionScope(condition, scope);

        return new WhileStatement(conditionScope);
    }
}