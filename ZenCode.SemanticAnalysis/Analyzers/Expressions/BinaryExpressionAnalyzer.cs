using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class BinaryExpressionAnalyzer
{
    public static Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, BinaryExpression binaryExpression)
    {
        var lType = semanticAnalyzer.Analyze(context, binaryExpression.Left);
        var rType = semanticAnalyzer.Analyze(context, binaryExpression.Right);

        if (lType != rType)
        {
            throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType);
        }

        Type type = binaryExpression.Operator.Type switch
        {
            TokenType.Plus => lType switch
            {
                IntegerType => new IntegerType(),
                FloatType => new FloatType(),
                StringType => new StringType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Minus => lType switch
            {
                IntegerType => new IntegerType(),
                FloatType => new FloatType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Multiplication => lType switch
            {
                IntegerType => new IntegerType(),
                FloatType => new FloatType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Division => lType switch
            {
                IntegerType => new IntegerType(),
                FloatType => new FloatType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Modulus => lType switch
            {
                IntegerType => new IntegerType(),
                FloatType => new FloatType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Exponentiation => lType switch
            {
                IntegerType => new IntegerType(),
                FloatType => new FloatType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.LessThan => lType switch
            {
                IntegerType => new BooleanType(),
                FloatType => new BooleanType(),
                StringType => new BooleanType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.LessThanOrEqual => lType switch
            {
                IntegerType => new BooleanType(),
                FloatType => new BooleanType(),
                StringType => new BooleanType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.GreaterThan => lType switch
            {
                IntegerType => new BooleanType(),
                FloatType => new BooleanType(),
                StringType => new BooleanType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.GreaterThanOrEqual => lType switch
            {
                IntegerType => new BooleanType(),
                FloatType => new BooleanType(),
                StringType => new BooleanType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Equals => new BooleanType(),
            TokenType.NotEquals => new BooleanType(),
            TokenType.And => lType switch
            {
                BooleanType => new IntegerType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            TokenType.Or => lType switch
            {
                BooleanType => new IntegerType(),
                _ => throw new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType)
            },
            _ => throw new ArgumentOutOfRangeException()
        };

        context.SetAstNodeType(binaryExpression, type);

        return type;
    }
}
