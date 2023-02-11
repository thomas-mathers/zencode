using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Statements;

namespace ZenCode.Parser.Parsers.Statements;

public interface IStatementParsingContext
{
    Statement Parse(ITokenStream tokenStream);
}