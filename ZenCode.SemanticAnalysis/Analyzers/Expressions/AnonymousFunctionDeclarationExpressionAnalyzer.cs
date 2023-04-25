using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class AnonymousFunctionDeclarationExpressionAnalyzer
{
    public static Type Analyze
    (
        ISemanticAnalyzer semanticAnalyzer,
        ISemanticAnalyzerContext context,
        AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression
    )
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(anonymousFunctionDeclarationExpression);
        
        context.PushEnvironment();

        semanticAnalyzer.Analyze(context, anonymousFunctionDeclarationExpression.Parameters);
        semanticAnalyzer.Analyze(context, anonymousFunctionDeclarationExpression.Body);

        context.PopEnvironment();

        var type = new FunctionType
        (
            anonymousFunctionDeclarationExpression.ReturnType,
            new TypeList
            (
                anonymousFunctionDeclarationExpression.Parameters.Parameters.Select(t => t.Type).ToArray()
            )
        );

        context.SetAstNodeType(anonymousFunctionDeclarationExpression, type);

        return type;
    }
}
