using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record NewExpression(Type Type, ExpressionList ExpressionList) : Expression
{
    public override string ToString()
    {
        return $"new {Type}[{ExpressionList}]";
    }
}