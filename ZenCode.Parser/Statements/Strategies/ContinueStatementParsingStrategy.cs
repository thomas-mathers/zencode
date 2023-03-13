using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ContinueStatementParsingStrategy : IContinueStatementParsingStrategy
{
    public ContinueStatement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Continue);
        return new ContinueStatement();
    }
}