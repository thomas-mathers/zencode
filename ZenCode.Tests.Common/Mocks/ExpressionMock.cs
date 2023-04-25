using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Tests.Common.Mocks;

public record ExpressionMock : Expression
{
    public override string ToString()
    {
        return "{Expression}";
    }
}
