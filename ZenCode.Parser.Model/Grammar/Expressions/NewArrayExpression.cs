using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record NewArrayExpression : Expression
{
    public Type Type { get; }
    public Expression Size { get; }

    public NewArrayExpression(Type type, Expression size)
    {
        ArgumentNullException.ThrowIfNull(type);
        
        Type = type;
        Size = size;
    }

    public override string ToString()
    {
        return $"new {Type}[{Size}]";
    }
}