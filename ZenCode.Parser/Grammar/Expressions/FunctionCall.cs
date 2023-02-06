using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record FunctionCall(Token Identifier, IReadOnlyList<Expression> Parameters) : Expression
{
    public virtual bool Equals(FunctionCall? other)
    {
        if (other is null)
        {
            return false;
        }

        return Identifier.Equals(other.Identifier) && Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Identifier, Parameters);
    }
}