using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions;

public class VariableReferenceParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionListParser> _expressionListParserMock = new();
    private readonly VariableReferenceParsingStrategy _sut;

    public VariableReferenceParsingStrategyTests()
    {
        _sut = new VariableReferenceParsingStrategy(_expressionListParserMock.Object);
    }

    [Fact]
    public void Parse_Identifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier)
        });

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_ZeroDimensionalArrayReference_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        // Act + Assert
        Assert.Throws<MissingIndexExpressionException>(() => _sut.Parse(tokenStream));
    }

    [Fact]
    public void Parse_SingleDimensionalArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.None),
            new Token(TokenType.RightBracket)
        });

        var indices = _fixture.CreateMany<Expression>(1).ToArray();
        
        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = indices
        };

        _expressionListParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(indices)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_MultiDimensionalArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.None),
            new Token(TokenType.RightBracket)
        });

        var indices = _fixture.CreateMany<Expression>(3).ToList();
        
        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = indices
        };

        _expressionListParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(indices)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}