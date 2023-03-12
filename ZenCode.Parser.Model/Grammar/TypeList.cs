using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar;

public record TypeList
{
    public IReadOnlyList<Type> Types { get; init; } = Array.Empty<Type>();

    public virtual bool Equals(TypeList? other)
    {
        return other != null && Types.SequenceEqual(other.Types);
    }

    public override int GetHashCode()
    {
        return Types.GetHashCode();
    }
}