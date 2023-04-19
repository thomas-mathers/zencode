using System.Text;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record AnonymousFunctionDeclarationExpression : Expression
{
    public required Type ReturnType { get; init; }
    public ParameterList Parameters { get; init; } = new();
    public Scope Body { get; init; } = new();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("function");
        stringBuilder.Append(' ');
        stringBuilder.Append('(');
        stringBuilder.Append(Parameters);
        stringBuilder.Append(')');
        stringBuilder.Append(' ');
        stringBuilder.Append("=>");
        stringBuilder.Append(' ');
        stringBuilder.Append(ReturnType);
        stringBuilder.AppendLine();
        stringBuilder.Append(Body);

        return stringBuilder.ToString();
    }
}
