using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class ScopeAnalyzer : IScopeAnalyzer
{
    public Type Analyze(ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, Scope scope)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(scope);
        
        context.PushEnvironment();

        foreach (var s in scope.Statements.Where(IsDeclarationStatement))
        {
            semanticAnalyzer.Analyze(context, s);
        }

        foreach (var s in scope.Statements.Where(IsNotDeclarationStatement))
        {
            semanticAnalyzer.Analyze(context, s);
        }

        context.PopEnvironment();

        return new VoidType();
    }

    private static bool IsNotDeclarationStatement(Statement s)
    {
        return s is not VariableDeclarationStatement and not FunctionDeclarationStatement;
    }

    private static bool IsDeclarationStatement(Statement s)
    {
        return s is VariableDeclarationStatement or FunctionDeclarationStatement;
    }
}
