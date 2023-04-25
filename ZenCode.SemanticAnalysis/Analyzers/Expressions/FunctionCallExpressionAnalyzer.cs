using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class FunctionCallExpressionAnalyzer
{
    public static Type Analyze
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
            throw new InvokingNonFunctionTypeException();
        }

        if (functionType.ParameterTypes.Types.Count != functionCallExpression.Arguments.Expressions.Count)
        {
            throw new IncorrectNumberOfParametersException
                (functionType.ParameterTypes.Types.Count, functionCallExpression.Arguments.Expressions.Count);
        }

        for (var i = 0; i < functionType.ParameterTypes.Types.Count; i++)
        {
            var parameterType = functionType.ParameterTypes.Types[i];
            var argumentType = semanticAnalyzer.Analyze(context, functionCallExpression.Arguments.Expressions[i]);

            if (parameterType != argumentType)
            {
                throw new TypeMismatchException(parameterType, argumentType);
            }
        }

        context.SetAstNodeType(functionCallExpression, functionType.ReturnType);

        return functionType.ReturnType;
    }
}
