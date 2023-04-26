using ZenCode.Lexer.Model;
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
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, BinaryExpression binaryExpression)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(binaryExpression);
        
        var lType = semanticAnalyzer.Analyze(context, binaryExpression.Left);
        var rType = semanticAnalyzer.Analyze(context, binaryExpression.Right);

        if (lType != rType)
        {
            context.AddError(new BinaryOperatorUnsupportedTypesException(binaryExpression.Operator.Type, lType, rType));
            
            return new UnknownType();
        }

        var type = binaryExpression.Operator.Type switch
        {
            TokenType.Plus => AnalyzePlusExpression(context, lType),
            TokenType.Minus => AnalyzeArithmeticExpression(context, lType),
            TokenType.Multiplication => AnalyzeArithmeticExpression(context, lType),
            TokenType.Division => AnalyzeArithmeticExpression(context, lType),
            TokenType.Modulus => AnalyzeArithmeticExpression(context, lType),
            TokenType.Exponentiation => AnalyzeArithmeticExpression(context, lType),
            TokenType.LessThan => AnalyzeComparisonExpression(context, lType),
            TokenType.LessThanOrEqual => AnalyzeComparisonExpression(context, lType),
            TokenType.GreaterThan => AnalyzeComparisonExpression(context, lType),
            TokenType.GreaterThanOrEqual => AnalyzeComparisonExpression(context, lType),
            TokenType.Equals => new BooleanType(),
            TokenType.NotEquals => new BooleanType(),
            TokenType.And => AnalyzeLogicalExpression(context, lType),
            TokenType.Or => AnalyzeLogicalExpression(context, lType),
            _ => throw new ArgumentOutOfRangeException()
        };

        context.SetAstNodeType(binaryExpression, type);

        return type;
    }
    
    private static Type AnalyzePlusExpression(ISemanticAnalyzerContext context, Type operandType)
    {
        if (operandType is IntegerType or FloatType or StringType)
        {
            return operandType;
        }

        context.AddError(new BinaryOperatorUnsupportedTypesException(TokenType.Plus, operandType, operandType));
            
        return new UnknownType();
    }
    
    private static Type AnalyzeArithmeticExpression(ISemanticAnalyzerContext context, Type operandType)
    {
        if (operandType is IntegerType or FloatType)
        {
            return operandType;
        }
        
        context.AddError(new BinaryOperatorUnsupportedTypesException(TokenType.Plus, operandType, operandType));
        
        return new UnknownType();
    }
    
    private static Type AnalyzeComparisonExpression(ISemanticAnalyzerContext context, Type operandType)
    {
        if (operandType is IntegerType or FloatType or StringType)
        {
            return new BooleanType();
        }
        
        context.AddError(new BinaryOperatorUnsupportedTypesException(TokenType.Plus, operandType, operandType));
        
        return new UnknownType();
    }

    private static Type AnalyzeLogicalExpression(ISemanticAnalyzerContext context, Type operandType)
    {
        if (operandType is BooleanType)
        {
            return operandType;
        }
        
        context.AddError(new BinaryOperatorUnsupportedTypesException(TokenType.Plus, operandType, operandType));
        
        return new UnknownType();
    }
}
