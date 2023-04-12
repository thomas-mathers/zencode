using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ForStatement
(
    VariableDeclarationStatement Initialization,
    Expression Condition,
    AssignmentStatement Iterator,
    Scope Scope
) : CompoundStatement
{
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("for (");
        stringBuilder.Append(Initialization);
        stringBuilder.Append("; ");
        stringBuilder.Append(Condition);
        stringBuilder.Append("; ");
        stringBuilder.Append(Iterator);
        stringBuilder.Append(')');
        stringBuilder.AppendLine();
        stringBuilder.Append(Scope);

        return stringBuilder.ToString();
    }
}
