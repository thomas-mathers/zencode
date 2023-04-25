using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class IfStatementAnalyzer : IIfStatementAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, IfStatement ifStatement)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(ifStatement);
        
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
