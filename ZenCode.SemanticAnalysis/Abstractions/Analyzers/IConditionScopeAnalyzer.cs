using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers;

public interface IConditionScopeAnalyzer
{
    Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, ConditionScope conditionScope);
}
