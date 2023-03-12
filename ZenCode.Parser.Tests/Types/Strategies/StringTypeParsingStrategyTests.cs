using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class StringTypeParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly StringTypeParsingStrategy _sut = new();

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
}