using AutoFixture;
using Xunit;
using ZenCode.Lexer.Exceptions;

namespace ZenCode.Lexer.Tests;

public class TokenStreamTests
{
    private readonly Fixture _fixture = new();
    
    [Fact]
    public void Consume_DifferentTokenTypes_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var expectedToken = new Token
        {
            Type = TokenType.Addition
        };
        
        var sut = new TokenStream(new[] {expectedToken});
        
        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => sut.Consume(TokenType.Subtraction));
    }
    
    [Fact]
    public void Consume_EmptyStream_ThrowsInvalidOperationException()
    {
        // Arrange
        var sut = new TokenStream(Enumerable.Empty<Token>());
        
        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => sut.Consume());
    }

    [Fact]
    public void Consume_NonEmptyStream_ReturnsExpectedTokens()
    {
        // Arrange
        var expectedToken1 = _fixture.Create<Token>();
        var expectedToken2 = _fixture.Create<Token>();
        var expectedToken3 = _fixture.Create<Token>();
        
        var sut = new TokenStream(new[] { expectedToken1, expectedToken2, expectedToken3 });
        
        // Act
        var actualToken1 = sut.Consume();
        var actualToken2 = sut.Consume();
        var actualToken3 = sut.Consume();
        
        // Assert
        Assert.Equal(expectedToken1, actualToken1);
        Assert.Equal(expectedToken2, actualToken2);
        Assert.Equal(expectedToken3, actualToken3);
    }

    [Fact]
    public void ToList_NonEmptyList_ReturnsSameList()
    {
        // Arrange
        var expectedTokens = _fixture.CreateMany<Token>().ToList();
        
        var sut = new TokenStream(expectedTokens);
        
        // Act
        var actualTokens = sut.ToList();
        
        // Assert
        Assert.Equal(expectedTokens, actualTokens);
    }
    
    [Fact]
    public void ToList_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        var expectedTokens = Array.Empty<Token>();
        
        var sut = new TokenStream(expectedTokens);
        
        // Act
        var actualTokens = sut.ToList();
        
        // Assert
        Assert.Equal(expectedTokens, actualTokens);
    }
    
    [Fact]
    public void Peek_OffsetLargerThanNumberOfRemainingElements_ReturnsNull()
    {
        // Arrange
        var expectedTokens = _fixture.CreateMany<Token>(3).ToList();

        var sut = new TokenStream(expectedTokens);
        
        // Act
        var actualToken = sut.Peek(3);
        
        // Assert
        Assert.Null(actualToken);
    }

    [Fact]
    public void Peek_ZeroOffset_ReturnsFirstToken()
    {
        // Arrange
        var expectedToken = _fixture.Create<Token>();

        var sut = new TokenStream(new[] { expectedToken });
        
        // Act
        var actualToken = sut.Peek(0);

        // Assert
        Assert.Equal(expectedToken, actualToken);
    }
    
    [Fact]
    public void Peek_FromLastToFirst_ReturnsExpectedTokens()
    {
        // Arrange
        var expectedTokens = _fixture.CreateMany<Token>(5).ToList();

        var sut = new TokenStream(expectedTokens);
        
        // Act
        var actualToken5 = sut.Peek(4);
        var actualToken4 = sut.Peek(3);
        var actualToken3 = sut.Peek(2);
        var actualToken2 = sut.Peek(1);
        var actualToken1 = sut.Peek(0);
        var actualTokens = new[] { actualToken1, actualToken2, actualToken3, actualToken4, actualToken5 };

        // Assert
        Assert.Equal(expectedTokens, actualTokens);
    }
    
    [Fact]
    public void Peek_FromFirstToLast_ReturnsExpectedTokens()
    {
        // Arrange
        var expectedTokens = _fixture.CreateMany<Token>(5).ToList();

        var sut = new TokenStream(expectedTokens);
        
        // Act
        var actualToken1 = sut.Peek(0);
        var actualToken2 = sut.Peek(1);
        var actualToken3 = sut.Peek(2);
        var actualToken4 = sut.Peek(3);
        var actualToken5 = sut.Peek(4);
        var actualTokens = new[] { actualToken1, actualToken2, actualToken3, actualToken4, actualToken5 };

        // Assert
        Assert.Equal(expectedTokens, actualTokens);
    }
}