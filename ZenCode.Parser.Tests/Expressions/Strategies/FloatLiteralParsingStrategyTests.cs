using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class FloatLiteralParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly FloatLiteralParsingStrategy _sut;

    public FloatLiteralParsingStrategyTests()
    {
        _sut = new FloatLiteralParsingStrategy();
    }

    [Fact]
    public void Parse_FloatLiteral_ReturnsConstantExpression()
    {
        // Arrange
        var expected = new LiteralExpression(new Token(TokenType.FloatLiteral));

        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.FloatLiteral))
            .Returns(new Token(TokenType.FloatLiteral));

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}