using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class IfStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;
    private readonly IStatementParser _statementParser;

    public IfStatementParsingStrategy(IExpressionParser expressionParser, IStatementParser statementParser)
    {
        _expressionParser = expressionParser;
        _statementParser = statementParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.If);
        
        var thenConditionScope = ParseConditionScope(tokenStream);

        var elseIfConditionScopes = new List<ConditionScope>();

        while (tokenStream.Match(TokenType.ElseIf))
        {
            var elseIfConditionScope = ParseConditionScope(tokenStream);

            elseIfConditionScopes.Add(elseIfConditionScope);
        }

        Scope? elseScope = null;

        if (tokenStream.Match(TokenType.Else))
        {
            elseScope = _statementParser.ParseScope(tokenStream);
        }

        return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
    }

    private ConditionScope ParseConditionScope(ITokenStream tokenStream)
    {
        var condition = _expressionParser.ParseExpression(tokenStream);
        var scope = _statementParser.ParseScope(tokenStream);

        return new ConditionScope(condition, scope);
    }
}