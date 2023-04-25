using ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;

public interface IWhileStatementAnalyzer
{
    Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, WhileStatement whileStatement);
}
