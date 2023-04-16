namespace ZenCode.SemanticAnalysis.Exceptions;

public class SemanticAnalysisException : Exception
{
    public int? Line { get; }
    public int? Column { get; }

    public SemanticAnalysisException()
    {
    }
    
    public SemanticAnalysisException(string message) : base(message)
    {
    }
    
    public SemanticAnalysisException(int line, int column, string message) : base(message)
    {
        Line = line;
        Column = column;
    }
}
