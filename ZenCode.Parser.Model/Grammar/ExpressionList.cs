using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar
{
    public record ExpressionList
    {
        public IReadOnlyList<Expression> Expressions { get; init; } = Array.Empty<Expression>();

        public virtual bool Equals(ExpressionList? other)
        {
            return other != null && Expressions.SequenceEqual(other.Expressions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Expressions);
        }
    }
}