using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParser
{
    Statement Parse(ITokenStream tokenStream);
    Scope ParseScope(ITokenStream tokenStream);
}