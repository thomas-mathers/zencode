using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis.Abstractions;

public interface ISemanticAnalyzerContext
{
    void DefineSymbol(Symbol symbol);
    Symbol? ResolveSymbol(string identifier);
    void PushEnvironment();
    void PopEnvironment();
    void PushAstNode(AstNode node);
    void PopAstNode();
    IEnumerable<AstNode> AncestorAstNodes();
    void SetAstNodeType(AstNode node, Type type);
    Type GetAstNodeType(AstNode node);
}
