using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class NewArrayExpressionAnalyzer
{
    public static Type Analyze(ISemanticAnalyzerContext context, NewArrayExpression newArrayExpression)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(newArrayExpression);
        
        var type = new ArrayType(newArrayExpression.Type);

        context.SetAstNodeType(newArrayExpression, type);

        return type;
    }
}
