using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies
{
    public interface IReturnStatementParsingStrategy
    {
        ReturnStatement Parse(IParser parser, ITokenStream tokenStream);
    }
}