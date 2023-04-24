using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Analyzers.Statements;

public static class ReadStatementAnalyzer
{
    public static Type Analyze()
    {
        return new VoidType();
    }
}
