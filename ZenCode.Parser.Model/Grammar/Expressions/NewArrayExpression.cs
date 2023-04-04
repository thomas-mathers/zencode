using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record NewArrayExpression : Expression
{
    public NewArrayExpression(Type type, Expression size)
    {
        ArgumentNullException.ThrowIfNull(type);

        Type = type;
        Size = size;
    }

    public Type Type { get; }
    public Expression Size { get; }

    public override string ToString()
    {
        return $"new {Type}[{Size}]";
    }
}