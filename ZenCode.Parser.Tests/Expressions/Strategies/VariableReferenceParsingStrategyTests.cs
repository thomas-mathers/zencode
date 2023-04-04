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

public class VariableReferenceParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly VariableReferenceParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public VariableReferenceParsingStrategyTests()
    {
        _sut = new VariableReferenceParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void Parse_Identifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier));

        _tokenStreamMock.Setup(x => x.Consume(TokenType.Identifier)).Returns(new Token(TokenType.Identifier));

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_ArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var expected = _fixture.Create<VariableReferenceExpression>();

        _tokenStreamMock.Setup(x => x.Consume(TokenType.Identifier)).Returns(expected.Identifier);

        _tokenStreamMock.Setup(x => x.Match(TokenType.LeftBracket)).Returns(true);

        _parserMock.Setup(x => x.ParseArrayIndexExpressionList(_tokenStreamMock.Object)).Returns(expected.Indices);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}