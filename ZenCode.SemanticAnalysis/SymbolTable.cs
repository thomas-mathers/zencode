using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis;

public class SymbolTable
{
    private readonly Stack<Environment> _environments = new();

    public IEnumerable<Environment> Environments => _environments;

    public SymbolTable()
    {
        PushEnvironment(null);
    }

    public void PushEnvironment(Statement? statement)
    {
        _environments.Push(new Environment(statement));
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
        
        var existingSymbol = ResolveSymbol(symbol.Token.Text);
        
        if (existingSymbol != null)
        {
            throw new DuplicateIdentifierException(existingSymbol.Token);
        }

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
