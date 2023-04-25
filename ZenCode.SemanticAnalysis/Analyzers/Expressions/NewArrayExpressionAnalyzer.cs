using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class NewArrayExpressionAnalyzer : INewArrayExpressionAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context, NewArrayExpression newArrayExpression)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(newArrayExpression);
        
        var type = new ArrayType(newArrayExpression.Type);

        context.SetAstNodeType(newArrayExpression, type);

        return type;
    }
}
