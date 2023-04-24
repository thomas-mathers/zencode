using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class VariableDeclarationStatementAnalyzer
{
    public static Type Analyze
    (
        ISemanticAnalyzer semanticAnalyzer,
        ISemanticAnalyzerContext context,
        VariableDeclarationStatement variableDeclarationStatement
    )
    {
        var type = semanticAnalyzer.Analyze(context, variableDeclarationStatement.Value);

        var symbol = new Symbol(variableDeclarationStatement.VariableName, type);

        context.DefineSymbol(symbol);

        return new VoidType();
    }
}
