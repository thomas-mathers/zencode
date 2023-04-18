using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis.Tests;

public class SymbolTableTests
{
    private readonly SymbolTable _sut = new();
    
    [Fact]
    public void PushEnvironment_NoEnvironments_PushesEnvironment()
    {
        // Act
        _sut.PushEnvironment();
        
        // Assert
        Assert.Equal(2, _sut.EnvironmentCount);
    }
    
    [Fact]
    public void PopEnvironment_OneEnvironment_ThrowsInvalidOperationException()
    {
        // Act
        var exception = Assert.Throws<InvalidOperationException>(() => _sut.PopEnvironment());
        
        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void PopEnvironment_TwoEnvironments_PopsEnvironment()
    {
        // Arrange
        _sut.PushEnvironment();
        
        // Act
        _sut.PopEnvironment();
        
        // Assert
        Assert.Equal(1, _sut.EnvironmentCount);
    }
    
    [Fact]
    public void DefineSymbol_NullSymbol_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.DefineSymbol(null));
        
        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void DefineSymbol_SymbolNotDefined_DoesNotThrow()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        // Act
        _sut.DefineSymbol(symbol);
    }
    
    [Fact]
    public void DefineSymbol_SymbolAlreadyDefined_ThrowsDuplicateIdentifierException()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        _sut.DefineSymbol(symbol);
        
        // Act
        var exception = Assert.Throws<DuplicateIdentifierException>(() => _sut.DefineSymbol(symbol));
        
        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void DefineSymbol_SymbolAlreadyDefinedInParentEnvironment_DoesNotThrow()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        _sut.PushEnvironment();
        _sut.DefineSymbol(symbol);
        _sut.PopEnvironment();
        
        // Act
        _sut.DefineSymbol(symbol);
    }
    
    [Fact]
    public void ResolveSymbol_NullIdentifier_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.ResolveSymbol(null));
        
        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void ResolveSymbol_SymbolNotDefined_ReturnsNull()
    {
        // Act
        var symbol = _sut.ResolveSymbol("x");
        
        // Assert
        Assert.Null(symbol);
    }
    
    [Fact]
    public void ResolveSymbol_SymbolDefined_ReturnsSymbol()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        _sut.DefineSymbol(symbol);
        
        // Act
        var result = _sut.ResolveSymbol("x");
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(symbol, result);
    }
    
    [Fact]
    public void ResolveSymbol_SymbolDefinedInParentEnvironment_ReturnsSymbol()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        _sut.DefineSymbol(symbol);
        _sut.PushEnvironment();
        _sut.DefineSymbol(new Symbol(new Token(TokenType.Identifier, "y"), new StringType()));
        
        // Act
        var result = _sut.ResolveSymbol("x");
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(symbol, result);
    }
    
    [Fact]
    public void ResolveSymbol_SymbolDefinedInRemovedEnvironment_ReturnsNull()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        _sut.PushEnvironment();
        _sut.DefineSymbol(symbol);
        _sut.PopEnvironment();
        
        // Act
        var result = _sut.ResolveSymbol("x");
        
        // Assert
        Assert.Null(result);
    }
}
