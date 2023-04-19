using System.Text;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record ForStatement : CompoundStatement
{
    public required VariableDeclarationStatement Initializer { get; init; }
    public required Expression Condition { get; init; }
    public required AssignmentStatement Iterator { get; init; }
    public Scope Body { get; init; } = new();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("for (");
        stringBuilder.Append(Initializer);
        stringBuilder.Append("; ");
        stringBuilder.Append(Condition);
        stringBuilder.Append("; ");
        stringBuilder.Append(Iterator);
        stringBuilder.Append(')');
        stringBuilder.AppendLine();
        stringBuilder.Append(Body);

        return stringBuilder.ToString();
    }
}
