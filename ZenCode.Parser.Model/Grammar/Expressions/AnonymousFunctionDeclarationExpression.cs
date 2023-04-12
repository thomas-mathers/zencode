using System.Text;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record AnonymousFunctionDeclarationExpression
    (Type ReturnType, ParameterList Parameters, Scope Scope) : Expression
{
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
        stringBuilder.Append(Scope);

        return stringBuilder.ToString();
    }
}
