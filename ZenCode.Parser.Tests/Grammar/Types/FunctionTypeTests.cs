using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Tests.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class FunctionTypeTests
{
    [Fact]
    public void Constructor_NullReturnType_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>(() => new FunctionType(null!, It.IsAny<TypeList>()));
    }

    [Fact]
    public void Constructor_NullParameterList_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>(() => new FunctionType(It.IsAny<Type>(), null!));
    }

    [Fact]
    public void ToString_NoParameters_ReturnsCorrectString()
    {
        // Arrange
        var functionType = new FunctionType(new TypeMock(), new TypeList());

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

        var functionType = new FunctionType(new TypeMock(), parameterTypes);

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

        var functionType = new FunctionType(new TypeMock(), parameterTypes);

        // Act
        var actual = functionType.ToString();

        // Assert
        Assert.Equal("({Type}, {Type}, {Type}) => {Type}", actual);
    }
}
