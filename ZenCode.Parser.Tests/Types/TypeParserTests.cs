using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types;

namespace ZenCode.Parser.Tests.Types;

public class TypeParserTests
{
    private readonly Mock<IPrefixTypeParsingStrategy> _voidTypeParsingStrategyMock = new();
    private readonly Mock<IPrefixTypeParsingStrategy> _booleanTypeParsingStrategyMock = new();
    private readonly Mock<IPrefixTypeParsingStrategy> _integerTypeParsingStrategyMock = new();
    private readonly Mock<IPrefixTypeParsingStrategy> _floatTypeParsingStrategyMock = new();
    private readonly Mock<IPrefixTypeParsingStrategy> _stringTypeParsingStrategyMock = new();
    private readonly Mock<IInfixTypeParsingStrategy> _arrayTypeParsingStrategyMock = new();
    private readonly TypeParser _sut = new();

    public TypeParserTests()
    {
        _sut.SetPrefixTypeParsingStrategy(TokenType.Void, _voidTypeParsingStrategyMock.Object);
        _sut.SetPrefixTypeParsingStrategy(TokenType.Boolean, _booleanTypeParsingStrategyMock.Object);
        _sut.SetPrefixTypeParsingStrategy(TokenType.Integer, _integerTypeParsingStrategyMock.Object);
        _sut.SetPrefixTypeParsingStrategy(TokenType.Float, _floatTypeParsingStrategyMock.Object);
        _sut.SetPrefixTypeParsingStrategy(TokenType.String, _stringTypeParsingStrategyMock.Object);

        _sut.SetInfixTypeParsingStrategy(TokenType.LeftBracket, _arrayTypeParsingStrategyMock.Object);
    }
    
    [Fact]
    public void Parse_VoidType_CallsVoidTypeParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Void)
        });

        var expectedType = new VoidType();

        _voidTypeParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedType);
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_BooleanType_CallsBooleanTypeParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Boolean)
        });

        var expectedType = new BooleanType();

        _booleanTypeParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedType);
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_IntegerType_CallsIntegerTypeParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Integer)
        });

        var expectedType = new IntegerType();

        _integerTypeParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedType);
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_FloatType_CallsFloatTypeParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Float)
        });

        var expectedType = new FloatType();

        _floatTypeParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedType);
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
    
    [Fact]
    public void Parse_StringType_CallsStringTypeParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.String)
        });

        var expectedType = new StringType();

        _stringTypeParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedType);
        
        // Act
        var actualType = _sut.ParseType(tokenStream);
        
        // Assert
        Assert.Equal(expectedType, actualType);
    }
}