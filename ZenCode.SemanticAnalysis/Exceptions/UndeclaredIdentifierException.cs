using ZenCode.Lexer.Model;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class UndeclaredIdentifierException : SemanticAnalysisException
{
    public UndeclaredIdentifierException(Token token) : base($"Variable {token.Text} has not been defined")
    {
    }
}
