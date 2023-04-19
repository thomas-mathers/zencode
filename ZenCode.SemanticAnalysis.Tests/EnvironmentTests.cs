using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Exceptions;

namespace ZenCode.SemanticAnalysis.Tests;

public class EnvironmentTests
{
    private readonly Environment _sut = new(null);
    
    [Fact]
    public void DefineSymbol_NullSymbol_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.DefineSymbol(null!));
        
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
    public void ResolveSymbol_NullIdentifier_ThrowsArgumentNullException()
    {
        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => _sut.ResolveSymbol(null!));
        
        // Assert
        Assert.NotNull(exception);
    }

    
    [Fact]
    public void ResolveSymbol_SymbolNotDefined_ReturnsNull()
    {
        // Act
        var actual = _sut.ResolveSymbol("x");
        
        // Assert
        Assert.Null(actual);
    }
    
    [Fact]
    public void ResolveSymbol_SymbolDefined_ReturnsSymbol()
    {
        // Arrange
        var symbol = new Symbol(new Token(TokenType.Identifier, "x"), new IntegerType());
        
        _sut.DefineSymbol(symbol);

        // Act
        var actual = _sut.ResolveSymbol("x");
        
        // Assert
        Assert.Equal(symbol, actual);
    }
}
