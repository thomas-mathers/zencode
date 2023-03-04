using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParser
{
    Scope ParseScope(ITokenStream tokenStream);
    ConditionScope ParseConditionScope(ITokenStream tokenStream);
}