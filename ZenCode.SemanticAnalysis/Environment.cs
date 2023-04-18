using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis;

public class Environment
{
    private readonly IDictionary<string, Symbol> _symbols;

    public Environment()
    {
        _symbols = new Dictionary<string, Symbol>();
    }
    
    public Environment(IDictionary<string, Symbol> symbols)
    {
        ArgumentNullException.ThrowIfNull(symbols);
        
        _symbols = symbols;
    }
    
    public void DefineSymbol(Symbol symbol)
    {
        ArgumentNullException.ThrowIfNull(symbol);
        
        if (_symbols.TryGetValue(symbol.Token.Text, out var value))
        {
            throw new DuplicateIdentifierException(value.Token);
        }

        _symbols[symbol.Token.Text] = symbol;
    }

    public Symbol? ResolveSymbol(string identifier)
    {
        ArgumentNullException.ThrowIfNull(identifier);
        
        return _symbols.TryGetValue(identifier, out var value) ? value : null;
    }
}
