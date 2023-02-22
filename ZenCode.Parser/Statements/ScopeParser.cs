using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class ScopeParser : IScopeParser
{
    private readonly IStatementParser _statementParser;

    public ScopeParser(IStatementParser statementParser)
    {
        _statementParser = statementParser;
    }
    
    public Scope Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftBrace);

        var statements = new List<Statement>();
        
        while (!tokenStream.Match(TokenType.RightBrace))
        {
            statements.Add(_statementParser.Parse(tokenStream));
        }

        return new Scope { Statements = statements };
    }
}