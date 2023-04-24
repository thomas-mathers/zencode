using ZenCode.Parser.Model.Grammar;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public class SemanticAnalyzerContext : ISemanticAnalyzerContext
{
    private readonly SymbolTable _symbolTable = new();
    private readonly Stack<AstNode> _path = new();
    private readonly Dictionary<AstNode, Type> _type = new();

    public void DefineSymbol(Symbol symbol)
    {
        _symbolTable.DefineSymbol(symbol);
    }
    
    public Symbol? ResolveSymbol(string identifier)
    {
        return _symbolTable.ResolveSymbol(identifier);
    }

    public void PushEnvironment()
    {
        _symbolTable.PushEnvironment();
    }

    public void PopEnvironment()
    {
        _symbolTable.PopEnvironment();
    }

    public void PushAstNode(AstNode node)
    {
        _path.Push(node);
    }
    
    public void PopAstNode()
    {
        _path.Pop();
    }
    
    public IEnumerable<AstNode> AncestorAstNodes()
    {
        return _path;
    }

    public void SetAstNodeType(AstNode node, Type type)
    {
        _type[node] = type;
    }
    
    public Type GetAstNodeType(AstNode node)
    {
        return _type[node];
    }
}
