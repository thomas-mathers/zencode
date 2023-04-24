using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class WhileStatementAnalyzer
{
    public static Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, WhileStatement whileStatement)
    {
        semanticAnalyzer.Analyze(context, whileStatement.ConditionScope);

        return new VoidType();
    }
}
