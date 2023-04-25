using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers;

public interface IParameterAnalyzer
{
    Type Analyze(ISemanticAnalyzerContext context, Parameter parameter);
}
