using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IIfStatementParsingStrategy
{
    IfStatement Parse(IParser parser, ITokenStream tokenStream);
}