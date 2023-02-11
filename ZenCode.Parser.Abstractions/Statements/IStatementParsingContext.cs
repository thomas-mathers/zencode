using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParsingContext
{
    Statement Parse(ITokenStream tokenStream);
}