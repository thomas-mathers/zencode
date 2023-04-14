using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ContinueStatementParsingStrategy : IContinueStatementParsingStrategy
{
    public ContinueStatement Parse(ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.Continue);

        return new ContinueStatement();
    }
}
