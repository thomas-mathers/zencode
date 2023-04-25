using ZenCode.Parser.Model.Grammar.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;

public interface ILiteralExpressionAnalyzer
{
    Type Analyze(ISemanticAnalyzerContext context, LiteralExpression literalExpression);
}
