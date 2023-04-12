using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IBreakStatementParsingStrategy
{
    BreakStatement Parse(ITokenStream tokenStream);
}
