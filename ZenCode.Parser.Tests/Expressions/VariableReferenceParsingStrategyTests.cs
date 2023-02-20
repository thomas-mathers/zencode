using Moq;
using Xunit;
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
            new Token(TokenType.Integer),
            new Token(TokenType.RightBracket)
        });

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(TokenType.Integer)))
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new[]
            {
                new ConstantExpression(new Token(TokenType.Integer))
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
            new Token(TokenType.Integer),
            new Token(TokenType.Comma),
            new Token(TokenType.Integer),
            new Token(TokenType.Comma),
            new Token(TokenType.Integer),
            new Token(TokenType.RightBracket)
        });

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(TokenType.Integer)))
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new[]
            {
                new ConstantExpression(new Token(TokenType.Integer)),
                new ConstantExpression(new Token(TokenType.Integer)),
                new ConstantExpression(new Token(TokenType.Integer))
            }
        };
            
        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}