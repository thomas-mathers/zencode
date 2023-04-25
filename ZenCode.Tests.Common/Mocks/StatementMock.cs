using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Tests.Common.Mocks;

public record StatementMock : Statement
{
    public override string ToString()
    {
        return "{Statement}";
    }
}
