using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IScopeParser
{
    Scope ParseScope(IParser parser, ITokenStream tokenStream);
}