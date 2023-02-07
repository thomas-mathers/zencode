using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Statements;

namespace ZenCode.Parser.Parsers.Statements;

public interface IStatementParser
{
    Statement Parse(ITokenStream tokenStream);
}