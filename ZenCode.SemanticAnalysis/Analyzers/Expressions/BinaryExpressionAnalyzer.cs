using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class BinaryExpressionAnalyzer : IBinaryExpressionAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, BinaryExpression expression)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(expression);

        var lType = semanticAnalyzer.Analyze(context, expression.Left);
        var rType = semanticAnalyzer.Analyze(context, expression.Right);

        if (lType != rType)
        {
            context.AddError(new BinaryOperatorUnsupportedTypesException(expression.Operator, lType, rType));

            return new UnknownType();
        }

        var type = expression.Operator switch
        {
            BinaryOperatorType.Addition => AnalyzePlusExpression(context, expression.Operator, lType),
            BinaryOperatorType.Subtraction => AnalyzeArithmeticExpression(context, expression.Operator, lType),
            BinaryOperatorType.Multiplication => AnalyzeArithmeticExpression(context, expression.Operator, lType),
            BinaryOperatorType.Division => AnalyzeArithmeticExpression(context, expression.Operator, lType),
            BinaryOperatorType.Modulo => AnalyzeArithmeticExpression(context, expression.Operator, lType),
            BinaryOperatorType.Power => AnalyzeArithmeticExpression(context, expression.Operator, lType),
            BinaryOperatorType.LessThan => AnalyzeComparisonExpression(context, expression.Operator, lType),
            BinaryOperatorType.LessThanOrEqual => AnalyzeComparisonExpression(context, expression.Operator, lType),
            BinaryOperatorType.GreaterThan => AnalyzeComparisonExpression(context, expression.Operator, lType),
            BinaryOperatorType.GreaterThanOrEqual => AnalyzeComparisonExpression(context, expression.Operator, lType),
            BinaryOperatorType.Equals => new BooleanType(),
            BinaryOperatorType.NotEquals => new BooleanType(),
            BinaryOperatorType.And => AnalyzeLogicalExpression(context, expression.Operator, lType),
            BinaryOperatorType.Or => AnalyzeLogicalExpression(context, expression.Operator, lType),
            _ => throw new ArgumentOutOfRangeException()
        };

        context.SetAstNodeType(expression, type);

        return type;
    }

    private static Type AnalyzePlusExpression
        (ISemanticAnalyzerContext context, BinaryOperatorType operatorType, Type operandType)
    {
        if (operandType is IntegerType or FloatType or StringType)
        {
            return operandType;
        }

        context.AddError(new BinaryOperatorUnsupportedTypesException(operatorType, operandType, operandType));

        return new UnknownType();
    }

    private static Type AnalyzeArithmeticExpression
        (ISemanticAnalyzerContext context, BinaryOperatorType operatorType, Type operandType)
    {
        if (operandType is IntegerType or FloatType)
        {
            return operandType;
        }

        context.AddError(new BinaryOperatorUnsupportedTypesException(operatorType, operandType, operandType));

        return new UnknownType();
    }

    private static Type AnalyzeComparisonExpression
        (ISemanticAnalyzerContext context, BinaryOperatorType operatorType, Type operandType)
    {
        if (operandType is IntegerType or FloatType or StringType)
        {
            return new BooleanType();
        }

        context.AddError(new BinaryOperatorUnsupportedTypesException(operatorType, operandType, operandType));

        return new UnknownType();
    }

    private static Type AnalyzeLogicalExpression
        (ISemanticAnalyzerContext context, BinaryOperatorType operatorType, Type operandType)
    {
        if (operandType is BooleanType)
        {
            return operandType;
        }

        context.AddError(new BinaryOperatorUnsupportedTypesException(operatorType, operandType, operandType));

        return new UnknownType();
    }
}
