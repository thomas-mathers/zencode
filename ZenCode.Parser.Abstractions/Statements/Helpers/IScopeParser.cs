using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements.Helpers;

public interface IScopeParser
{
    Scope Parse(ITokenStream tokenStream);
}