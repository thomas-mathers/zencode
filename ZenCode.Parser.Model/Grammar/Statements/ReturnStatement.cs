using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ReturnStatement : SimpleStatement
{
    public Expression? Value { get; init; } = null;

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("return");

        if (Value != null)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append(Value);
        }

        return stringBuilder.ToString();
    }
}
