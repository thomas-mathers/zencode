using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar;

public record ConditionScope : AstNode
{
    public required Expression Condition { get; init; }
    public Scope Scope { get; init; } = new();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append('(');
        stringBuilder.Append(Condition);
        stringBuilder.Append(')');
        stringBuilder.AppendLine();
        stringBuilder.Append(Scope);

        return stringBuilder.ToString();
    }
}
