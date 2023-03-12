using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public interface IWhileStatementParsingStrategy
{
    WhileStatement Parse(IParser parser, ITokenStream tokenStream);
}