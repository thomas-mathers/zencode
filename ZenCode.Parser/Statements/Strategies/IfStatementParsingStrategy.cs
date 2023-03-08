using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class IfStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public IfStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.If);
        
        var thenConditionScope = _parser.ParseConditionScope(tokenStream);

        var elseIfConditionScopes = new List<ConditionScope>();

        while (tokenStream.Match(TokenType.ElseIf))
        {
            tokenStream.Consume(TokenType.ElseIf);
            
            var elseIfConditionScope = _parser.ParseConditionScope(tokenStream);

            elseIfConditionScopes.Add(elseIfConditionScope);
        }

        if (!tokenStream.Match(TokenType.Else))
        {
            return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes };
        }
        
        tokenStream.Consume(TokenType.Else);
            
        var elseScope = _parser.ParseScope(tokenStream);

        return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
    }
}