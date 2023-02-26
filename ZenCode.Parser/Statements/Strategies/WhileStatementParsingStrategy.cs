using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements.Helpers;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class WhileStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IConditionScopeParser _conditionScopeParser;

    public WhileStatementParsingStrategy(IConditionScopeParser conditionScopeParser)
    {
        _conditionScopeParser = conditionScopeParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.While);

        var conditionScope = _conditionScopeParser.Parse(tokenStream);
        
        return new WhileStatement(conditionScope);
    }
}