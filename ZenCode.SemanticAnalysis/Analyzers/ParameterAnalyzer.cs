using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers;

public class ParameterAnalyzer : IParameterAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context, Parameter parameter)
    {
        context.DefineSymbol(new Symbol(parameter.Identifier, parameter.Type));

        return new VoidType();
    }
}
