using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record NewArrayExpression : Expression
{
    public required Type Type { get; init; }
    public required Expression Size { get; init; }
    
    public override string ToString()
    {
        return $"new {Type}[{Size}]";
    }
}
