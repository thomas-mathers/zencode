using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Exceptions;

internal class TypeMismatchException : SemanticAnalysisException
{
    public TypeMismatchException(Type expectedType, Type receivedType) : base("")
    {
        
    }
}
