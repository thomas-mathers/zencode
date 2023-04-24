using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class IfStatementAnalyzer
{
    public static Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, IfStatement ifStatement)
    {
        semanticAnalyzer.Analyze(context, ifStatement.ThenScope);

        foreach (var conditionScope in ifStatement.ElseIfScopes)
        {
            semanticAnalyzer.Analyze(context, conditionScope);
        }

        if (ifStatement.ElseScope != null)
        {
            semanticAnalyzer.Analyze(context, ifStatement.ElseScope);
        }

        return new VoidType();
    }
}
