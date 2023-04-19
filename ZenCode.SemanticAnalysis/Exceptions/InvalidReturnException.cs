namespace ZenCode.SemanticAnalysis.Exceptions;

public class InvalidReturnException : SemanticAnalysisException
{
    public InvalidReturnException() : base
        ("Return statement can only be used inside a function")
    {
    }
}
