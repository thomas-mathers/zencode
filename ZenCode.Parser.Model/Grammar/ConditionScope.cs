using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar;

public record ConditionScope(Expression Condition, Scope Scope) : AstNode
{
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