using Xunit;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class IntegerTypeTests
{
    [Fact]
    public void ToString_IntegerType_ReturnsCorrectString()
    {
        // Arrange + Act + Assert
        Assert.Equal("integer", new IntegerType().ToString());
    }
}