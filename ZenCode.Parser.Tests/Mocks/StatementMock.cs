using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Mocks;

public record StatementMock : Statement
{
    public override string ToString()
    {
        return "{Statement}";
    }
}
