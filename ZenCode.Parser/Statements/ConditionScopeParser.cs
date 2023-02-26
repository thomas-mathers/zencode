using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements.Helpers;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements;

public class ConditionScopeParser : IConditionScopeParser
{
    private readonly IExpressionParser _expressionParser;
    private readonly IScopeParser _scopeParser;
    
    public ConditionScopeParser(IExpressionParser expressionParser, IScopeParser scopeParser)
    {
        _expressionParser = expressionParser;
        _scopeParser = scopeParser;
    }
    
    public ConditionScope Parse(ITokenStream tokenStream)
    {
        var condition = _expressionParser.Parse(tokenStream);
        var scope = _scopeParser.Parse(tokenStream);

        return new ConditionScope(condition, scope);
    }
}