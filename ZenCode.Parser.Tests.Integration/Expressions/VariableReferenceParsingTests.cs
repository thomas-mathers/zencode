using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Integration.Expressions;

public class VariableReferenceParsingTests
{
    private readonly IParser _parser;

    public VariableReferenceParsingTests()
    {
        _parser = new ParserFactory().Create();
    }
    
    [Fact]
    public void ParseExpression_VariableReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier)
            }
        );

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier));

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_ArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void ParseExpression_MissingIndexExpression_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _parser.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Unexpected token ']'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_MissingLastDimensionIndexExpression_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _parser.ParseExpression(tokenStream));

        // Assert
        Assert.Equal("Unexpected token ']'", exception.Message);
    }
    
    [Fact]
    public void ParseExpression_ArrayReferenceMultipleIndices_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket)
            }
        );

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList
            (
                new LiteralExpression(new Token(TokenType.IntegerLiteral)),
                new LiteralExpression(new Token(TokenType.IntegerLiteral))
            )
        };

        // Act
        var actual = _parser.ParseExpression(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
