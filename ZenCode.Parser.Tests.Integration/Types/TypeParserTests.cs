using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types;

namespace ZenCode.Parser.Tests.Integration.Types;

public class TypeParserTests
{
    private readonly ITypeParser _sut;

    public TypeParserTests()
    {
        _sut = new TypeParserFactory().Create();
    }
    
    [Fact]
    public void Parse_VoidType_ReturnsVoidType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Void)
        });

        var expectedType = new VoidType();
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_BooleanType_ReturnsBooleanType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Boolean)
        });

        var expectedType = new BooleanType();
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_IntegerType_ReturnsIntegerType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Integer)
        });

        var expectedType = new IntegerType();
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_FloatType_ReturnsFloatType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Float)
        });

        var expectedType = new FloatType();
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_StringType_ReturnsStringType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.String)
        });

        var expectedType = new StringType();
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
}