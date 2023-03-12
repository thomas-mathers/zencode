using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class ArrayTypeParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly ArrayTypeParsingStrategy _sut = new();

    [Fact]
    public void Parse_SomeBaseType_ReturnsArrayType()
    {
        // Arrange
        var expectedType = _fixture.Create<ArrayType>();

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object, expectedType.BaseType);
        
        // Assert
        Assert.Equal(expectedType, actual);
    }
}