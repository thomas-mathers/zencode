using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies
{
    public interface IIfStatementParsingStrategy
    {
        IfStatement Parse(IParser parser, ITokenStream tokenStream);
    }
}