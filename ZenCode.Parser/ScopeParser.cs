using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser;

public class ScopeParser : IScopeParser
{
    public Scope ParseScope(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftBrace);

        var statements = new List<Statement>();

        while (true)
        {
            if (tokenStream.Match(TokenType.RightBrace))
            {
                tokenStream.Consume(TokenType.RightBrace);
                break;
            }
                
            statements.Add(parser.ParseStatement(tokenStream));
        }

        return new Scope { Statements = statements };
    }
}