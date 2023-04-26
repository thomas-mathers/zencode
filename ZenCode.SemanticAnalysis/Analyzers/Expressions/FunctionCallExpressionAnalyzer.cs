using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class FunctionCallExpressionAnalyzer : IFunctionCallExpressionAnalyzer
{
    public Type Analyze
    (
        ISemanticAnalyzer semanticAnalyzer,
        ISemanticAnalyzerContext context,
        FunctionCallExpression functionCallExpression
    )
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(functionCallExpression);
        
        var expressionType = semanticAnalyzer.Analyze(context, functionCallExpression.FunctionReference);

        if (expressionType is not FunctionType functionType)
        {
            context.AddError(new InvokingNonFunctionTypeException());

            return new UnknownType();
        }

        if (functionType.ParameterTypes.Types.Count != functionCallExpression.Arguments.Expressions.Count)
        {
            context.AddError(new IncorrectNumberOfParametersException
                (functionType.ParameterTypes.Types.Count, functionCallExpression.Arguments.Expressions.Count));

            return new UnknownType();
        }

        for (var i = 0; i < functionType.ParameterTypes.Types.Count; i++)
        {
            var parameterType = functionType.ParameterTypes.Types[i];
            var argumentType = semanticAnalyzer.Analyze(context, functionCallExpression.Arguments.Expressions[i]);

            if (parameterType != argumentType)
            {
                context.AddError(new TypeMismatchException(parameterType, argumentType));
            }
        }

        context.SetAstNodeType(functionCallExpression, functionType.ReturnType);

        return functionType.ReturnType;
    }
}
