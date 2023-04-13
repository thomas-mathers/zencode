namespace ZenCode.Parser.Model.Grammar;

public record ParameterList : AstNode
{
    public IReadOnlyList<Parameter> Parameters { get; init; } = Array.Empty<Parameter>();

    public ParameterList()
    {
        
    }
    
    public ParameterList(params Parameter[] parameters)
    {
        Parameters = parameters;
    }

    public virtual bool Equals(ParameterList? other)
    {
        return other != null && Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        return Parameters.GetHashCode();
    }

    public override string ToString()
    {
        return string.Join(", ", Parameters);
    }
}
