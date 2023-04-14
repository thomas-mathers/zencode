using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class StringTypeParsingStrategyTests
{
    private readonly StringTypeParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    [Fact]
    public void Parse_String_ReturnsStringType()
    {
        // Arrange
        var expected = new StringType();

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.String));
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
}
