using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Statements;

namespace ZenCode.Parser.Parsers.Statements;

public interface IStatementParsingStrategy
{
    Statement Parse(ITokenStream tokenStream);
}