using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class ConstantParsingStrategyTests
{
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly ConstantParsingStrategy _sut;

    public ConstantParsingStrategyTests()
    {
        _sut = new ConstantParsingStrategy();
    }

    [Theory]
    [ClassData(typeof(Constants))]
    public void Parse_Constant_ReturnsConstantExpression(TokenType tokenType)
    {
        // Arrange
        var expected = new ConstantExpression(new Token(tokenType));

        _tokenStreamMock
            .Setup(x => x.Consume())
            .Returns(new Token(tokenType));

        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}