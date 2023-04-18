namespace ZenCode.SemanticAnalysis;

public class SymbolTable : ISymbolTable
{
    private readonly Stack<Environment> _environments = new();

    public int EnvironmentCount => _environments.Count;

    public SymbolTable()
    {
        PushEnvironment();
    }

    public void PushEnvironment()
    {
        _environments.Push(new Environment());
    }

    public void PopEnvironment()
    {
        if (_environments.Count == 1)
        {
            throw new InvalidOperationException();
        }

        _environments.Pop();
    }

    public void DefineSymbol(Symbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);
        
        _environments.Peek().DefineSymbol(symbol);
    }

    public Symbol? ResolveSymbol(string identifier)
    {
        foreach (var environment in _environments)
        {
            var symbol = environment.ResolveSymbol(identifier);

            if (symbol != null)
            {
                return symbol;
            }
        }

        return null;
    }
}
