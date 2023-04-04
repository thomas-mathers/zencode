using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Mocks;

public record TypeMock : Type
{
    public override string ToString()
    {
        return "{Type}";
    }
}