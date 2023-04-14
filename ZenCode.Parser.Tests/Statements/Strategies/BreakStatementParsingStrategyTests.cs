using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class BreakStatementParsingStrategyTests
{
    private readonly BreakStatementParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    [Fact]
    public void Parse_Break_ReturnsBreakStatement()
    {
        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.IsType<BreakStatement>(actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.Break));
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
