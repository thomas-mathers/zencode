using ZenCode.Lexer;

namespace ZenCode.Parser.Grammar.Expressions;

public record FunctionCall(Token Identifier, IReadOnlyList<Expression> Parameters) : Expression
{
    public virtual bool Equals(FunctionCall? other)
    {
        return other != null && Identifier.Equals(other.Identifier) && Parameters.SequenceEqual(other.Parameters);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Identifier, Parameters);
    }
}