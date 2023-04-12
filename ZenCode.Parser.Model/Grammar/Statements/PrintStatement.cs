using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Model.Grammar.Statements;

public record PrintStatement(Expression Expression) : SimpleStatement
{
    public override string ToString()
    {
        return $"print {Expression}";
    }
}
