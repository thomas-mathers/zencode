using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Exceptions;

public class UnaryOperatorUnsupportedTypeException : SemanticAnalysisException
{
    public UnaryOperatorUnsupportedTypeException(Token unaryExpressionOperator, Type type) : base
    (
        unaryExpressionOperator.Line,
        unaryExpressionOperator.StartingColumn,
        $"{unaryExpressionOperator} is not supported for type {type}"
    )
    {
    }
}
