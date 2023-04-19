namespace ZenCode.SemanticAnalysis.Exceptions;

public class InvalidContinueException : SemanticAnalysisException
{
    public InvalidContinueException() : base
        ("Continue statement can only be used inside a loop")
    {
    }
}
