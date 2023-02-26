using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements.Helpers;

public interface IConditionScopeParser
{
    ConditionScope Parse(ITokenStream tokenStream);
}