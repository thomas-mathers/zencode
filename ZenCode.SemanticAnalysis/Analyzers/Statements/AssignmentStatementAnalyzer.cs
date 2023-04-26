using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class AssignmentStatementAnalyzer : IAssignmentStatementAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, AssignmentStatement assignmentStatement)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(assignmentStatement);
        
        var symbol = context.ResolveSymbol(assignmentStatement.VariableReference.Identifier.Text);

        if (symbol == null)
        {
            context.AddError(new UndeclaredIdentifierException(assignmentStatement.VariableReference.Identifier));

            return new VoidType();
        }

        var expressionType = semanticAnalyzer.Analyze(context, assignmentStatement.Value);

        if (symbol.Type != expressionType)
        {
            context.AddError(new TypeMismatchException(symbol.Type, expressionType));
        }

        return new VoidType();
    }
}
