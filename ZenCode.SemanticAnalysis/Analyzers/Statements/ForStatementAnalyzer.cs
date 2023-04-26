using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class ForStatementAnalyzer : IForStatementAnalyzer
{
    public Type Analyze
        (ISemanticAnalyzer semanticAnalyzer, ISemanticAnalyzerContext context, ForStatement forStatement)
    {
        ArgumentNullException.ThrowIfNull(semanticAnalyzer);
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(forStatement);
        
        context.PushEnvironment();

        semanticAnalyzer.Analyze(context, forStatement.Initializer);

        var conditionType = semanticAnalyzer.Analyze(context, forStatement.Condition);

        if (conditionType is not BooleanType)
        {
            context.AddError(new TypeMismatchException(new BooleanType(), conditionType));
        }

        semanticAnalyzer.Analyze(context, forStatement.Iterator);
        semanticAnalyzer.Analyze(context, forStatement.Body);

        context.PopEnvironment();

        return new VoidType();
    }
}
