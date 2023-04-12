using ZenCode.Lexer.Model;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class DuplicateVariableDeclarationException : Exception
{
    public DuplicateVariableDeclarationException(Token token) : base($"Variable {token.Text} has been defined")
    {
    }
}
