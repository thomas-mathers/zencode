using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Model.Grammar;

public record Program : AstNode
{
    public Scope Scope { get; } = new();
    
    public Program(Scope scope)
    {
        Scope = scope;
    }
    
    public Program(params Statement[] statements)
    {
        Scope = new Scope(statements);
    }

    public override string ToString()
    {
        return Scope.ToString();
    }
}
