using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;
using ZenCode.Tests.Common.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class ArrayTypeParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly ArrayTypeParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public ArrayTypeParsingStrategyTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Type), typeof(TypeMock)));
    }

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
    
    [Fact]
    public void Parse_UnexpectedToken_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var baseType = _fixture.Create<Type>();
        
        _tokenStreamMock
            .Setup(x => x.Consume(It.IsAny<TokenType>()))
            .Throws<UnexpectedTokenException>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>
        (
            () => _sut.Parse(_tokenStreamMock.Object, baseType)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullTokenStream_ThrowsArgumentNullException()
    {
        // Arrange
        var baseType = _fixture.Create<Type>();
        
        ITokenStream? tokenStream = null;

        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(tokenStream!, baseType)
        );

        // Assert
        Assert.NotNull(actual);
    }
}
