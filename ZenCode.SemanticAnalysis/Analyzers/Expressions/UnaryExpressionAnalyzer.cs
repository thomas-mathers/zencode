using ZenCode.Parser.Model.Grammar.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class UnaryExpressionAnalyzer
{
    public static Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, UnaryExpression unaryExpression)
    {
        var type = semanticAnalyzer.Analyze(context, unaryExpression.Expression);

        context.SetAstNodeType(unaryExpression, type);

        return type;
    }
}
