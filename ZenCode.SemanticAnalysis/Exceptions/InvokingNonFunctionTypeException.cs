namespace ZenCode.SemanticAnalysis.Exceptions;

public class InvokingNonFunctionTypeException : SemanticAnalysisException
{
    public InvokingNonFunctionTypeException() : base("Function is expected")
    {
    }
}
