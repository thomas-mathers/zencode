using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Tests.Common.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class FunctionTypeTests
{
    [Fact]
    public void ToString_NoParameters_ReturnsCorrectString()
    {
        // Arrange
        var functionType = new FunctionType { ReturnType = new TypeMock(), ParameterTypes = new TypeList() };

        // Act
        var actual = functionType.ToString();

        // Assert
        Assert.Equal("() => {Type}", actual);
    }

    [Fact]
    public void ToString_OneParameter_ReturnsCorrectString()
    {
        // Arrange
        var parameterTypes = new TypeList(new TypeMock());

        var functionType = new FunctionType
        {
            ReturnType = new TypeMock(),
            ParameterTypes = parameterTypes
        };

        // Act
        var actual = functionType.ToString();

        // Assert
        Assert.Equal("({Type}) => {Type}", actual);
    }

    [Fact]
    public void ToString_ThreeParameters_ReturnsCorrectString()
    {
        // Arrange
        var parameterTypes = new TypeList
        (
            new TypeMock(),
            new TypeMock(),
            new TypeMock()
        );

        var functionType = new FunctionType
        {
            ReturnType = new TypeMock(),
            ParameterTypes = parameterTypes
        };

        // Act
        var actual = functionType.ToString();

        // Assert
        Assert.Equal("({Type}, {Type}, {Type}) => {Type}", actual);
    }
}
