using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class IfStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IStatementParser _statementParser;

    public IfStatementParsingStrategy(IStatementParser statementParser)
    {
        _statementParser = statementParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.If);

        var thenConditionScope = _statementParser.ParseConditionScope(tokenStream);

        var elseIfConditionScopes = new List<ConditionScope>();

        while (tokenStream.Match(TokenType.ElseIf))
        {
            var elseIfConditionScope = _statementParser.ParseConditionScope(tokenStream);

            elseIfConditionScopes.Add(elseIfConditionScope);
        }

        Scope? elseScope = null;

        if (tokenStream.Match(TokenType.Else))
        {
            elseScope = _statementParser.ParseScope(tokenStream);
        }

        return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
    }
}