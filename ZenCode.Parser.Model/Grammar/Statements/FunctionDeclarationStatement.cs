using System.Text;
using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record FunctionDeclarationStatement
    (Type ReturnType, Token Identifier, ParameterList Parameters, Scope Scope) : CompoundStatement
{
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("function");
        stringBuilder.Append(' ');
        stringBuilder.Append(Identifier);
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
