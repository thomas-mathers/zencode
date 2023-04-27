using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class UnaryExpressionAnalyzer : IUnaryExpressionAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, UnaryExpression unaryExpression)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(unaryExpression);
        
        var type = semanticAnalyzer.Analyze(context, unaryExpression.Expression);

        switch (unaryExpression.Operator)
        {
            case UnaryOperatorType.Negate:
                if (type is not IntegerType and not FloatType)
                {
                    context.AddError(new UnaryOperatorUnsupportedTypeException(unaryExpression.Operator, type));
                }

                break;
            default:
                if (type is not BooleanType)
                {
                    context.AddError(new UnaryOperatorUnsupportedTypeException(unaryExpression.Operator, type));
                }

                break;
        }

        context.SetAstNodeType(unaryExpression, type);

        return type;
    }
}
