using ZenCode.Parser.Model.Grammar.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;

public interface IUnaryExpressionAnalyzer
{
    Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, UnaryExpression unaryExpression);
}
