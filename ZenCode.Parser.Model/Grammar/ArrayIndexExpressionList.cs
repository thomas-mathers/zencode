using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar;

public record ArrayIndexExpressionList : AstNode
{
    public IReadOnlyList<Expression> Expressions { get; init; } = Array.Empty<Expression>();

    public virtual bool Equals(ArrayIndexExpressionList? other)
    {
        return other != null && Expressions.SequenceEqual(other.Expressions);
    }

    public override int GetHashCode()
    {
        return Expressions.GetHashCode();
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        foreach (var expression in Expressions)
        {
            stringBuilder.Append('[');
            stringBuilder.Append(expression);
            stringBuilder.Append(']');
        }

        return stringBuilder.ToString();
    }
}