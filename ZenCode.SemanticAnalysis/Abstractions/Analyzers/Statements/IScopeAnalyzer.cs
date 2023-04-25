using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;

public interface IScopeAnalyzer
{
    Type Analyze(ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, Scope scope);
}
