using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class VoidTypeParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly VoidTypeParsingStrategy _sut = new();

    [Fact]
    public void Parse_Void_ReturnsVoidType()
    {
        // Arrange
        var expected = new VoidType();

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Void));
    }
}