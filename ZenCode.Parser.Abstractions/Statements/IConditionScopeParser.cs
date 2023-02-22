using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IConditionScopeParser
{
    ConditionScope Parse(ITokenStream tokenStream);
}