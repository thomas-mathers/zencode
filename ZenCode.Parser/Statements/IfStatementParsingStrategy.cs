using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class IfStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IConditionScopeParser _conditionScopeParser;
    private readonly IScopeParser _scopeParser;

    public IfStatementParsingStrategy(IConditionScopeParser conditionScopeParser, IScopeParser scopeParser)
    {
        _conditionScopeParser = conditionScopeParser;
        _scopeParser = scopeParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.If);
        
        var thenScope = _conditionScopeParser.Parse(tokenStream);

        var elseIfScopes = new List<ConditionScope>();
        
        while (tokenStream.Match(TokenType.ElseIf))
        {
            elseIfScopes.Add(_conditionScopeParser.Parse(tokenStream));
        }

        Scope? elseScope = null;
        
        if (tokenStream.Match(TokenType.Else))
        {
            elseScope = _scopeParser.Parse(tokenStream);
        }

        return new IfStatement(thenScope) { ElseIfScopes = elseIfScopes, ElseScope = elseScope };
    }
}