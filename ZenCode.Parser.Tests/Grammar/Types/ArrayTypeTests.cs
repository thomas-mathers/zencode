using Xunit;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class ArrayTypeTests
{
    [Fact]
    public void ToString_AnyBaseType_ReturnsCorrectString()
    {
        // Arrange
        var arrayType = new ArrayType { BaseType = new TypeMock() };

        // Act
        var actual = arrayType.ToString();

        // Assert
        Assert.Equal("{Type}[]", actual);
    }
}
