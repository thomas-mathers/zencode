namespace ZenCode.Parser.Model;

public record ParameterList
{
    public IReadOnlyList<Parameter> Parameters { get; init; } = Array.Empty<Parameter>();
}