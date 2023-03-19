using Xunit;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class VoidTypeTests
{
    [Fact]
    public void ToString_VoidType_ReturnsCorrectString()
    {
        // Arrange + Act + Assert
        Assert.Equal("void", new VoidType().ToString());
    }
}