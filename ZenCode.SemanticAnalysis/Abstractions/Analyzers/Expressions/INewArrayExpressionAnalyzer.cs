using ZenCode.Parser.Model.Grammar.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;

public interface INewArrayExpressionAnalyzer
{
    Type Analyze(ISemanticAnalyzerContext context, NewArrayExpression newArrayExpression);
}
