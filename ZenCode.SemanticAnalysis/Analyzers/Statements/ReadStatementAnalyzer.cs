using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions.Analyzers.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public class ReadStatementAnalyzer : IReadStatementAnalyzer
{
    public Type Analyze()
    {
        return new VoidType();
    }
}
