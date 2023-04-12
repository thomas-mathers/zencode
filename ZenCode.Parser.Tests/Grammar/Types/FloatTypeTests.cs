using Xunit;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class FloatTypeTests
{
    [Fact]
    public void ToString_FloatType_ReturnsCorrectString()
    {
        // Arrange + Act + Assert
        Assert.Equal("float", new FloatType().ToString());
    }
}
