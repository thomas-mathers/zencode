using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Abstractions.Statements;

public interface IStatementParser
{
    AssignmentStatement ParseAssignmentStatement(IParser parser, ITokenStream tokenStream);
    VariableDeclarationStatement ParseVariableDeclarationStatement(IParser parser, ITokenStream tokenStream);
    Statement ParseStatement(IParser parser, ITokenStream tokenStream);
}
