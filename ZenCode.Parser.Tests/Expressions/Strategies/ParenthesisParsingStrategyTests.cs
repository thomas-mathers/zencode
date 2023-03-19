using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class ParenthesisParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly ParenthesisParsingStrategy _sut;

    public ParenthesisParsingStrategyTests()
    {
        _sut = new ParenthesisParsingStrategy();
        
        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Expression),
                typeof(ExpressionMock)));
    }

    [Fact]
    public void Parse_ParenthesizedExpression_ReturnsConstantExpression()
    {
        // Arrange
        var expected = _fixture.Create<Expression>();

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expected);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.LeftParenthesis));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.RightParenthesis));
    }
}