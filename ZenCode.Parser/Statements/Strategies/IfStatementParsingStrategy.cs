using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
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

        var thenCondition = _expressionParser.Parse(tokenStream);
        var thenScope = _statementParser.ParseScope(tokenStream);
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseIfScopes = new List<ConditionScope>();
        
        while (tokenStream.Match(TokenType.ElseIf))
        {
            var elseIfCondition = _expressionParser.Parse(tokenStream);
            var elseIfScope = _statementParser.ParseScope(tokenStream);
            var elseIfConditionScope = new ConditionScope(elseIfCondition, elseIfScope);
            
            elseIfScopes.Add(elseIfConditionScope);
        }

        Scope? elseScope = null;
        
        if (tokenStream.Match(TokenType.Else))
        {
            elseScope = _statementParser.ParseScope(tokenStream);
        }

        return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfScopes, ElseScope = elseScope };
    }
}