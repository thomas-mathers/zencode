using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParser
{
    Statement ParseStatement(ITokenStream tokenStream);
    Scope ParseScope(ITokenStream tokenStream);
}