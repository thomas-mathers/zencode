using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;

public interface IBreakStatementAnalyzer
{
    Type Analyze(ISemanticAnalyzerContext context);
}
