using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class UnaryExpressionAnalyzer
{
    public static Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, UnaryExpression unaryExpression)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(unaryExpression);
        
        var type = semanticAnalyzer.Analyze(context, unaryExpression.Expression);

        switch (unaryExpression.Operator.Type)
        {
            case TokenType.Minus:
                if (type is not IntegerType and not FloatType)
                {
                    throw new UnaryOperatorUnsupportedTypeException(unaryExpression.Operator, type);
                }

                break;
            case TokenType.Not:
                if (type is not BooleanType)
                {
                    throw new UnaryOperatorUnsupportedTypeException(unaryExpression.Operator, type);
                }

                break;
        }

        context.SetAstNodeType(unaryExpression, type);

        return type;
    }
}
