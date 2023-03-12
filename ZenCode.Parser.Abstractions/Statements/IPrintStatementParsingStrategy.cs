using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IPrintStatementParsingStrategy
{
    PrintStatement Parse(IParser parser, ITokenStream tokenStream);
}