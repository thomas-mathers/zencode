using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class ContinueStatementAnalyzer
{
    public static Type Analyze(ISemanticAnalyzerContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        var loopStatement = context.AncestorAstNodes().FirstOrDefault(e => e is WhileStatement or ForStatement);

        if (loopStatement == null)
        {
            throw new InvalidContinueException();
        }

        return new VoidType();
    }
}
