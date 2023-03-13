using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public interface IContinueStatementParsingStrategy
{
    ContinueStatement Parse(ITokenStream tokenStream);
}