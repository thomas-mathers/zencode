using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class BreakStatementParsingStrategy : IBreakStatementParsingStrategy
{
    public BreakStatement Parse(ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.Break);

        return new BreakStatement();
    }
}
