using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class BooleanTypeParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly BooleanTypeParsingStrategy _sut = new();

    [Fact]
    public void Parse_Boolean_ReturnsBooleanType()
    {
        // Arrange
        var expected = new BooleanType();

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Boolean));
    }
}