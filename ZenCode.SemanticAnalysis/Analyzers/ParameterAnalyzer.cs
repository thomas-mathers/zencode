using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers;
using ZenCode.SemanticAnalysis.Exceptions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers;

public class ParameterAnalyzer : IParameterAnalyzer
{
    public Type Analyze(ISemanticAnalyzerContext context, Parameter parameter)
    {
        if (context.ResolveSymbol(parameter.Identifier.Text) != null)
        {
            context.AddError(new DuplicateIdentifierException(parameter.Identifier));
            
            return new VoidType();
        }
        
        context.DefineSymbol(new Symbol(parameter.Identifier, parameter.Type));

        return new VoidType();
    }
}
