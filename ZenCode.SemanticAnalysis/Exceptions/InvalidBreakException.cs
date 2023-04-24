namespace ZenCode.SemanticAnalysis.Exceptions;

public class InvalidBreakException : SemanticAnalysisException
{
    public InvalidBreakException() : base
        ("Break statement can only be used inside a loop")
    {
    }
}
