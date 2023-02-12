namespace ZenCode.Grammar.Expressions;

public record FunctionCall(VariableReferenceExpression VariableReferenceExpression, IReadOnlyList<Expression> Parameters) : Expression
{
    public virtual bool Equals(FunctionCall? other)
    {
        return other != null && VariableReferenceExpression.Equals(other.VariableReferenceExpression) && Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), VariableReferenceExpression, Parameters);
    }
}