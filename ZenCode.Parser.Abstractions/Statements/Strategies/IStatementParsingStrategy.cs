using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements.Strategies;

public interface IStatementParsingStrategy
{
    Statement Parse(ITokenStream tokenStream);
}