using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
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

        if (context.ResolveSymbol(variableDeclarationStatement.VariableName.Text) != null)
        {
            context.AddError(new DuplicateIdentifierException(variableDeclarationStatement.VariableName));
            
            return new VoidType();
        }
        
        context.DefineSymbol(new Symbol(variableDeclarationStatement.VariableName, type));

        return new VoidType();
    }
}
