using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public interface IVariableDeclarationStatementParsingStrategy
{
    VariableDeclarationStatement Parse(IParser parser, ITokenStream tokenStream);
}