using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ReturnStatement : SimpleStatement
{
    public Expression? Expression { get; init; } = null;

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("return");

        if (Expression != null)
        {
            stringBuilder.Append(' ');
            stringBuilder.Append(Expression);
        }

        return stringBuilder.ToString();
    }
}
