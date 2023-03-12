using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class IfStatementParsingStrategy
{
    public IfStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.If);
        
        var thenConditionScope = parser.ParseConditionScope(tokenStream);

        var elseIfConditionScopes = new List<ConditionScope>();

        while (tokenStream.Match(TokenType.ElseIf))
        {
            tokenStream.Consume(TokenType.ElseIf);
            
            var elseIfConditionScope = parser.ParseConditionScope(tokenStream);

            elseIfConditionScopes.Add(elseIfConditionScope);
        }

        if (!tokenStream.Match(TokenType.Else))
        {
            return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes };
        }
        
        tokenStream.Consume(TokenType.Else);
            
        var elseScope = parser.ParseScope(tokenStream);

        return new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
    }
}