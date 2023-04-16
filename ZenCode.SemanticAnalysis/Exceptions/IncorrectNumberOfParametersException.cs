namespace ZenCode.SemanticAnalysis.Exceptions;

public class IncorrectNumberOfParametersException : SemanticAnalysisException
{
    public IncorrectNumberOfParametersException(int numParametersExpected, int numParametersReceived) : base
        ($"Method expected {numParametersExpected} parameters but received {numParametersReceived}")
    {
    }
}
