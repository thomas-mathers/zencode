using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public abstract class ScopeAnalyzer
{
    public static Type Analyze(ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, Scope scope)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(scope);
        
        context.PushEnvironment();

        foreach (var s in scope.Statements.Where
            (s => s is VariableDeclarationStatement or FunctionDeclarationStatement))
        {
            semanticAnalyzer.Analyze(context, s);
        }

        foreach (var s in scope.Statements.Where
            (s => s is not VariableDeclarationStatement and not FunctionDeclarationStatement))
        {
            semanticAnalyzer.Analyze(context, s);
        }

        context.PopEnvironment();

        return new VoidType();
    }
}
