using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record VariableDeclarationStatement : SimpleStatement
{
    public required Token VariableName { get; init; }
    public required Expression Value { get; init; }

    public override string ToString()
    {
        return $"var {VariableName} := {Value}";
    }
}
