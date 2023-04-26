using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Expressions;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Expressions;

public class VariableReferenceExpressionAnalyzer : IVariableReferenceExpressionAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context, VariableReferenceExpression variableReferenceExpression)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(variableReferenceExpression);
        
        var symbol = context.ResolveSymbol(variableReferenceExpression.Identifier.Text);

        if (symbol == null)
        {
            context.AddError(new UndeclaredIdentifierException(variableReferenceExpression.Identifier));

            return new UnknownType();
        }

        var type = symbol.Type;

        context.SetAstNodeType(variableReferenceExpression, type);

        return type;
    }
}
