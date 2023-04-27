using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record PrintStatement : SimpleStatement
{
    public required Expression Expression { get; init; }

    public override string ToString()
    {
        return $"print {Expression}";
    }
}
