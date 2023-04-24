using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public static class VariableReferenceExpressionAnalyzer
{
    public static Type Analyze(ISemanticAnalyzerContext context, VariableReferenceExpression variableReferenceExpression)
    {
        var symbol = context.ResolveSymbol(variableReferenceExpression.Identifier.Text);

        if (symbol == null)
        {
            throw new UndeclaredIdentifierException(variableReferenceExpression.Identifier);
        }

        var type = symbol.Type;

        context.SetAstNodeType(variableReferenceExpression, type);

        return type;
    }
}
