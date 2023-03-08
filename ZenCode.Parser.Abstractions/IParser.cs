using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IParser : IExpressionParser, IStatementParser, ITypeParser
{
    public Program ParseProgram(ITokenStream tokenStream);
}