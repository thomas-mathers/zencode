using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types;
using Type = System.Type;

namespace ZenCode.Parser.Tests.Types;

public class TypeParserTests
{
    private readonly TypeParser _sut = new();

    [Fact]
    public void Parse_Boolean_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Boolean),
        });

        var expected = new BooleanType();
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_Integer_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Integer),
        });

        var expected = new IntegerType();
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_Float_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Float),
        });

        var expected = new FloatType();
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_String_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.String),
        });

        var expected = new StringType();
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_BooleanArray_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Boolean),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var expected = new ArrayType(new BooleanType());
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_IntegerArray_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Integer),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var expected = new ArrayType(new IntegerType());
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_FloatArray_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Float),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var expected = new ArrayType(new FloatType());
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_StringArray_ReturnsCorrectType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.String),
            new Token(TokenType.LeftBracket),
            new Token(TokenType.RightBracket)
        });

        var expected = new ArrayType(new StringType());
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}