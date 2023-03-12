using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IWhileStatementParsingStrategy
{
    WhileStatement Parse(IParser parser, ITokenStream tokenStream);
}