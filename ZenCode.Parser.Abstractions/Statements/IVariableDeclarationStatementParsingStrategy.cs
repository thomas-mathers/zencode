using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IVariableDeclarationStatementParsingStrategy
{
    VariableDeclarationStatement Parse(IParser parser, ITokenStream tokenStream);
}