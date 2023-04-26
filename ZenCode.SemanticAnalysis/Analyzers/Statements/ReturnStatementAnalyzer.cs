using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class ReturnStatementAnalyzer : IReturnStatementAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, ReturnStatement returnStatement)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(returnStatement);
        
        var functionDeclaration = context.AncestorAstNodes().FirstOrDefault
            (e => e is FunctionDeclarationStatement or AnonymousFunctionDeclarationExpression);

        if (functionDeclaration == null)
        {
            context.AddError(new InvalidReturnException());
            
            return new VoidType();
        }

        var returnType = returnStatement.Value == null
            ? new VoidType()
            : semanticAnalyzer.Analyze(context, returnStatement.Value);

        var functionReturnType = functionDeclaration switch
        {
            FunctionDeclarationStatement functionDeclarationStatement => functionDeclarationStatement.ReturnType,
            AnonymousFunctionDeclarationExpression anonymousFunctionDeclarationExpression =>
                anonymousFunctionDeclarationExpression.ReturnType,
            _ => throw new InvalidOperationException()
        };

        if (returnType != functionReturnType)
        {
            context.AddError(new TypeMismatchException(functionReturnType, returnType));
        }

        return new VoidType();
    }
}
