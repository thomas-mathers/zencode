using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class UnaryOperatorUnsupportedTypeException : SemanticAnalysisException
{
    public UnaryOperatorUnsupportedTypeException(UnaryOperatorType unaryExpressionOperator, Type type) : base
    (
        $"{unaryExpressionOperator} is not supported for type {type}"
    )
    {
    }
}
