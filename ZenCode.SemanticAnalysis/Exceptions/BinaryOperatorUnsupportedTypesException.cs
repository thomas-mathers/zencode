using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class BinaryOperatorUnsupportedTypesException : SemanticAnalysisException
{
    public BinaryOperatorUnsupportedTypesException(BinaryOperatorType op, Type leftType, Type rightType) : base
        ($"Operator {op} is not supported for types {leftType} and {rightType}")
    {
    }
}
