namespace ZenCode.Parser.Model.Grammar.Expressions;

public record FunctionCallExpression(VariableReferenceExpression VariableReferenceExpression) : Expression
{
    public IReadOnlyList<Expression> Arguments { get; init; } = Array.Empty<Expression>();

    public virtual bool Equals(FunctionCallExpression? other)
    {
        return other != null && VariableReferenceExpression.Equals(other.VariableReferenceExpression) &&
               Arguments.SequenceEqual(other.Arguments);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), VariableReferenceExpression, Arguments);
    }
}