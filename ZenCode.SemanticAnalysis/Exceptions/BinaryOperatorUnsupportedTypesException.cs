using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class BinaryOperatorUnsupportedTypesException : SemanticAnalysisException
{
    public BinaryOperatorUnsupportedTypesException(TokenType op, Type leftType, Type rightType) : base
        ($"Operator {op} is not supported for types {leftType} and {rightType}")
    {
    }
}
