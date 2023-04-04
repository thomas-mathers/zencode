using Xunit;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class StringTypeTests
{
    [Fact]
    public void ToString_StringType_ReturnsCorrectString()
    {
        // Arrange + Act + Assert
        Assert.Equal("string", new StringType().ToString());
    }
}