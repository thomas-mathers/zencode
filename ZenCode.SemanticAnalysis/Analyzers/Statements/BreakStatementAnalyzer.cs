using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class BreakStatementAnalyzer : IBreakStatementAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        var loopStatement = context.AncestorAstNodes().FirstOrDefault(IsLoopStatement);

        if (loopStatement == null)
        {
            context.AddError(new InvalidBreakException());
        }

        return new VoidType();
    }

    private static bool IsLoopStatement(AstNode e)
    {
        return e is WhileStatement or ForStatement;
    }
}
