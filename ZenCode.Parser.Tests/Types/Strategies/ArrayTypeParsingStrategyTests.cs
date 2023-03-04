using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class ArrayTypeParsingStrategyTests
{
    private readonly ArrayTypeParsingStrategy _sut = new(0);
    
    [Fact]
    public void Parse_VoidType_ReturnsArrayOfType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var baseType = new VoidType();
        
        var expected = new ArrayType(baseType);
        
        // Act
        var actual = _sut.Parse(tokenStream, baseType);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_BooleanType_ReturnsArrayOfType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var baseType = new BooleanType();
        
        var expected = new ArrayType(baseType);
        
        // Act
        var actual = _sut.Parse(tokenStream, baseType);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_IntegerType_ReturnsArrayOfType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var baseType = new IntegerType();
        
        var expected = new ArrayType(baseType);
        
        // Act
        var actual = _sut.Parse(tokenStream, baseType);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_FloatType_ReturnsArrayOfType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var baseType = new FloatType();
        
        var expected = new ArrayType(baseType);
        
        // Act
        var actual = _sut.Parse(tokenStream, baseType);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_StringType_ReturnsArrayOfType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var baseType = new StringType();
        
        var expected = new ArrayType(baseType);
        
        // Act
        var actual = _sut.Parse(tokenStream, baseType);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}