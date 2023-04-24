using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class TypeMismatchException : SemanticAnalysisException
{
    public TypeMismatchException(Type expectedType, Type receivedType) : base
        ($"Expected type {expectedType} but received type {receivedType}")
    {
    }
}
