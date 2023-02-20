using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Common.Testing.Extensions;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class VariableReferenceParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly VariableReferenceParsingStrategy _sut;

    public VariableReferenceParsingStrategyTests()
    {
        _sut = new VariableReferenceParsingStrategy(_expressionParserMock.Object);
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

        var expression = _fixture.Create<Expression>();

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(expression)
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new[]
            {
                expression
            }
        };

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
            new Token(TokenType.Comma),
            new Token(TokenType.None),
            new Token(TokenType.Comma),
            new Token(TokenType.None),
            new Token(TokenType.RightBracket)
        });

        var indexExpressions = _fixture.CreateMany<Expression>().ToList();
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .ReturnsSequence(indexExpressions)
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = indexExpressions
        };
            
        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}