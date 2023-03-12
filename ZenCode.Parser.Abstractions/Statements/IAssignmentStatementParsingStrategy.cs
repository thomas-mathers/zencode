using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies
{
    public interface IAssignmentStatementParsingStrategy
    {
        AssignmentStatement Parse(IParser parser, ITokenStream tokenStream);
    }
}