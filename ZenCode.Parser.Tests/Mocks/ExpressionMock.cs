using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Mocks;

public record ExpressionMock : Expression
{
    public override string ToString()
    {
        return "{Expression}";
    }
}