using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class FunctionDeclarationStatementAnalyzer : IFunctionDeclarationStatementAnalyzer
{
    public Type Analyze
    (
        ISemanticAnalyzer semanticAnalyzer,
        ISemanticAnalyzerContext context,
        FunctionDeclarationStatement functionDeclarationStatement
    )
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(functionDeclarationStatement);
        
        var type = new FunctionType
        (
            functionDeclarationStatement.ReturnType,
            new TypeList
            (
                functionDeclarationStatement.Parameters.Parameters.Select(parameter => parameter.Type).ToArray()
            )
        );

        if (context.ResolveSymbol(functionDeclarationStatement.Name.Text) != null)
        {
            context.AddError(new DuplicateIdentifierException(functionDeclarationStatement.Name));
            
            return new VoidType();
        }

        context.DefineSymbol(new Symbol(functionDeclarationStatement.Name, type));

        context.PushEnvironment();

        semanticAnalyzer.Analyze(context, functionDeclarationStatement.Parameters);
        semanticAnalyzer.Analyze(context, functionDeclarationStatement.Body);

        context.PopEnvironment();

        return new VoidType();
    }
}
