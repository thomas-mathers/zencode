namespace ZenCode.Parser.Model.Grammar;

public record ParameterList
{
    public IReadOnlyList<Parameter> Parameters { get; init; } = Array.Empty<Parameter>();

    public virtual bool Equals(ParameterList? other)
    {
        return other != null && Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        return Parameters.GetHashCode();
    }
}