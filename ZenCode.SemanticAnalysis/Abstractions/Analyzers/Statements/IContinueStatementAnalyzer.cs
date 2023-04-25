using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;

public interface IContinueStatementAnalyzer
{
    Type Analyze(ISemanticAnalyzerContext context);
}
