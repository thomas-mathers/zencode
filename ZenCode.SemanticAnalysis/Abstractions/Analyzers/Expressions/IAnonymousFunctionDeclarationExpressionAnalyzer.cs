using ZenCode.Parser.Model.Grammar.Expressions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;

public interface IAnonymousFunctionDeclarationExpressionAnalyzer
{
    Type Analyze
    (
        ISemanticAnalyzer semanticAnalyzer,
        ISemanticAnalyzerContext context,
        AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression
    );
}
