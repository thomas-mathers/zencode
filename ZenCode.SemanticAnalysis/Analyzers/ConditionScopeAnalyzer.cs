using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers;

public class ConditionScopeAnalyzer : IConditionScopeAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, ConditionScope conditionScope)
    {
        var conditionType = semanticAnalyzer.Analyze(context, conditionScope.Condition);

        if (conditionType is not BooleanType)
        {
            throw new TypeMismatchException(new BooleanType(), conditionType);
        }

        semanticAnalyzer.Analyze(context, conditionScope.Scope);

        return new VoidType();
    }
}
