using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers;

public class ParameterListAnalyzer : IParameterListAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, ParameterList parameterList)
    {
        foreach (var parameter in parameterList.Parameters)
        {
            semanticAnalyzer.Analyze(context, parameter);
        }

        return new VoidType();
    }
}
