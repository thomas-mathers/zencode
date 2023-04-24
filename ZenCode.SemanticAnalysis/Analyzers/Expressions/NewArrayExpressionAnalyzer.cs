using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class NewArrayExpressionAnalyzer
{
    public static Type Analyze(ISemanticAnalyzerContext context, NewArrayExpression newArrayExpression)
    {
        var type = new ArrayType(newArrayExpression.Type);

        context.SetAstNodeType(newArrayExpression, type);

        return type;
    }
}
