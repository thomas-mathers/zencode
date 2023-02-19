using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParser
{
    Statement Parse(ITokenStream tokenStream);
}