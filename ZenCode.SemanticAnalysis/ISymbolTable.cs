namespace ZenCode.SemanticAnalysis;

public interface ISymbolTable
{
    void PushEnvironment();
    void PopEnvironment();
    void DefineSymbol(Symbol symbol);
    Symbol? ResolveSymbol(string identifier);
}
