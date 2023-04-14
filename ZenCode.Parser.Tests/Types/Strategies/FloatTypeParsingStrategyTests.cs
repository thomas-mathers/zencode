using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class FloatTypeParsingStrategyTests
{
    private readonly FloatTypeParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    [Fact]
    public void Parse_Float_ReturnsFloatType()
    {
        // Arrange
        var expected = new FloatType();

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.Float));
    }
    
    [Fact]
    public void Parse_UnexpectedToken_ThrowsUnexpectedTokenException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Consume(It.IsAny<TokenType>()))
            .Throws<UnexpectedTokenException>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>
        (
            () => _sut.Parse(_tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullTokenStream_ThrowsArgumentNullException()
    {
        // Arrange
        ITokenStream? tokenStream = null;

        // Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(tokenStream!)
        );

        // Assert
        Assert.NotNull(actual);
    }
}
