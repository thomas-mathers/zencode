using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class IntegerLiteralParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly IntegerLiteralParsingStrategy _sut;

    public IntegerLiteralParsingStrategyTests()
    {
        _sut = new IntegerLiteralParsingStrategy();
    }

    [Fact]
    public void Parse_IntegerLiteral_ReturnsConstantExpression()
    {
        // Arrange
        var expected = new ConstantExpression(new Token(TokenType.IntegerLiteral));

        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.IntegerLiteral))
            .Returns(new Token(TokenType.IntegerLiteral));

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}