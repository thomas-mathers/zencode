using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class FunctionDeclarationStatementAnalyzer
{
    public static Type Analyze
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

        var symbol = new Symbol(functionDeclarationStatement.Name, type);

        context.DefineSymbol(symbol);

        context.PushEnvironment();

        semanticAnalyzer.Analyze(context, functionDeclarationStatement.Parameters);
        semanticAnalyzer.Analyze(context, functionDeclarationStatement.Body);

        context.PopEnvironment();

        return new VoidType();
    }
}
