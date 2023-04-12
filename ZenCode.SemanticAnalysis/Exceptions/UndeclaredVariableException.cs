using ZenCode.Lexer.Model;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class UndeclaredVariableException : Exception
{
    public UndeclaredVariableException(Token token) : base($"Variable {token.Text} has not been defined")
    {
    }
}
