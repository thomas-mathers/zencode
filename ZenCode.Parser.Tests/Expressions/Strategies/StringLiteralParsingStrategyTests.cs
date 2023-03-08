using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class StringLiteralParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly StringLiteralParsingStrategy _sut;

    public StringLiteralParsingStrategyTests()
    {
        _sut = new StringLiteralParsingStrategy();
    }

    [Fact]
    public void Parse_StringLiteral_ReturnsConstantExpression()
    {
        // Arrange
        var expected = new LiteralExpression(new Token(TokenType.StringLiteral));

        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.StringLiteral))
            .Returns(new Token(TokenType.StringLiteral));

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}