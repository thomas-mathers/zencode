using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class VariableDeclarationStatementAnalyzer : IVariableDeclarationStatementAnalyzer
{
    public Type Analyze
    (
        ISemanticAnalyzer semanticAnalyzer,
        ISemanticAnalyzerContext context,
        VariableDeclarationStatement variableDeclarationStatement
    )
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(variableDeclarationStatement);
        
        var type = semanticAnalyzer.Analyze(context, variableDeclarationStatement.Value);

        var symbol = new Symbol(variableDeclarationStatement.VariableName, type);

        context.DefineSymbol(symbol);

        return new VoidType();
    }
}
