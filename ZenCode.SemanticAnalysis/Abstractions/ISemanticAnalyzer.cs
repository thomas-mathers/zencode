using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions;

public interface ISemanticAnalyzer
{
    Type Analyze(ISemanticAnalyzerContext context, AstNode node);
}
