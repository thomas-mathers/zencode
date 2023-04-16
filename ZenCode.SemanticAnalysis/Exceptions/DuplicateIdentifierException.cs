using ZenCode.Lexer.Model;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class DuplicateIdentifierException : SemanticAnalysisException
{
    public DuplicateIdentifierException(Token token) : base($"Variable {token.Text} has been defined")
    {
    }
}
