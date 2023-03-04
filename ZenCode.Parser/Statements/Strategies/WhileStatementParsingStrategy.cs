using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class WhileStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IStatementParser _statementParser;

    public WhileStatementParsingStrategy(IStatementParser statementParser)
    {
        _statementParser = statementParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.While);

        var conditionScope = _statementParser.ParseConditionScope(tokenStream);

        return new WhileStatement(conditionScope);
    }
}