using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class AnonymousFunctionDeclarationExpressionAnalyzer : IAnonymousFunctionDeclarationExpressionAnalyzer
{
    public Type Analyze
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
        {
            ReturnType = anonymousFunctionDeclarationExpression.ReturnType,
            ParameterTypes = new TypeList
            (
                anonymousFunctionDeclarationExpression.Parameters.Parameters.Select(t => t.Type).ToArray()
            )
        };

        context.SetAstNodeType(anonymousFunctionDeclarationExpression, type);

        return type;
    }
}
