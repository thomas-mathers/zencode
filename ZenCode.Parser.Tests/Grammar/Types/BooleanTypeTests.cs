using Xunit;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class BooleanTypeTests
{
    [Fact]
    public void ToString_BooleanType_ReturnsCorrectString()
    {
        // Arrange + Act + Assert
        Assert.Equal("boolean", new BooleanType().ToString());
    }
}
