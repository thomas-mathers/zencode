using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers;

public static class ParameterAnalyzer
{
    public static Type Analyze(ISemanticAnalyzerContext context, Parameter parameter)
    {
        context.DefineSymbol(new Symbol(parameter.Identifier, parameter.Type));

        return new VoidType();
    }
}
