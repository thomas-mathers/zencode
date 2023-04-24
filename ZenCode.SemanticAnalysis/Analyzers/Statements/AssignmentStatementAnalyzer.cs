using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class AssignmentStatementAnalyzer
{
    public static Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, AssignmentStatement assignmentStatement)
    {
        var symbol = context.ResolveSymbol(assignmentStatement.VariableReference.Identifier.Text);

        if (symbol == null)
        {
            throw new UndeclaredIdentifierException(assignmentStatement.VariableReference.Identifier);
        }

        var expressionType = semanticAnalyzer.Analyze(context, assignmentStatement.Value);

        if (symbol.Type != expressionType)
        {
            throw new TypeMismatchException(symbol.Type, expressionType);
        }

        return new VoidType();
    }
}
