using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers;
using ZenCode.SemanticAnalysis.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;

namespace ZenCode.SemanticAnalysis;

public class SemanticAnalyzerFactory
{
    public ISemanticAnalyzer Create()
    {
        return new SemanticAnalyzer
        (
            new AssignmentStatementAnalyzer(),
            new BreakStatementAnalyzer(),
            new ContinueStatementAnalyzer(),
            new ForStatementAnalyzer(),
            new FunctionDeclarationStatementAnalyzer(),
            new IfStatementAnalyzer(),
            new PrintStatementAnalyzer(),
            new ReadStatementAnalyzer(),
            new ReturnStatementAnalyzer(),
            new VariableDeclarationStatementAnalyzer(),
            new WhileStatementAnalyzer(),
            new ScopeAnalyzer(),
            new AnonymousFunctionDeclarationExpressionAnalyzer(),
            new BinaryExpressionAnalyzer(),
            new FunctionCallExpressionAnalyzer(),
            new LiteralExpressionAnalyzer(),
            new NewArrayExpressionAnalyzer(),
            new UnaryExpressionAnalyzer(),
            new VariableReferenceExpressionAnalyzer(),
            new ConditionScopeAnalyzer(),
            new ParameterListAnalyzer(),
            new ParameterAnalyzer()
        );
    }
}
