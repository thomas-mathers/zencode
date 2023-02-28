using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class VariableReferenceParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly IExpressionParser _expressionParser = Mock.Of<IExpressionParser>();
    private readonly Mock<IArgumentListParser> _argumentListParserMock = new();
    private readonly VariableReferenceParsingStrategy _sut;

    public VariableReferenceParsingStrategyTests()
    {
        _sut = new VariableReferenceParsingStrategy(_expressionParser, _argumentListParserMock.Object);
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

        _argumentListParserMock
            .Setup(x => x.Parse(_expressionParser, tokenStream))
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

        _argumentListParserMock
            .Setup(x => x.Parse(_expressionParser, tokenStream))
            .Returns(indices)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}