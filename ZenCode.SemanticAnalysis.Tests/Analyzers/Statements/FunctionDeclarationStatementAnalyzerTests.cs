using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class FunctionDeclarationStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly FunctionDeclarationStatementAnalyzer _sut = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange
        var functionDeclarationStatement = new FunctionDeclarationStatement
        {
            ReturnType = new TypeMock(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList()
        };
        
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                null!,
                _semanticAnalyzerContextMock.Object,
                functionDeclarationStatement
            )
        );
    }
    
    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange
        var functionDeclarationStatement = new FunctionDeclarationStatement
        {
            ReturnType = new TypeMock(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList()
        };
        
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                null!,
                functionDeclarationStatement
            )
        );
    }
    
    [Fact]
    public void Analyze_NullFunctionDeclarationStatement_ThrowsArgumentNullException()
    {
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }
    
    [Fact]
    public void Analyze_FunctionDeclarationNoParameters_ReturnsVoidType()
    {
        // Arrange
        var functionDeclarationStatement = new FunctionDeclarationStatement
        {
            ReturnType = new TypeMock(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList()
        };

        var expectedType = new FunctionType(functionDeclarationStatement.ReturnType, new TypeList());
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            functionDeclarationStatement
        );
        
        // Assert
        Assert.Equal(new VoidType(), result);
        
        _semanticAnalyzerContextMock.Verify(x => x.DefineSymbol(It.Is<Symbol>(s => s.Type == expectedType)));
    }
    
    [Fact]
    public void Analyze_FunctionDeclarationOneParameter_ReturnsVoidType()
    {
        // Arrange
        var functionDeclarationStatement = new FunctionDeclarationStatement
        {
            ReturnType = new TypeMock(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList
            {
                Parameters = new[]
                {
                    new Parameter(new Token(TokenType.Identifier), new TypeMock())
                }
            }
        };

        var expectedType = new FunctionType
        (
            functionDeclarationStatement.ReturnType,
            new TypeList
            (
                functionDeclarationStatement.Parameters.Parameters.Select(parameter => parameter.Type).ToArray()
            )
        );
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            functionDeclarationStatement
        );
        
        // Assert
        Assert.Equal(new VoidType(), result);
        
        _semanticAnalyzerContextMock.Verify(x => x.DefineSymbol(It.Is<Symbol>(s => s.Type == expectedType)));
    }
    
    [Fact]
    public void Analyze_FunctionDeclarationThreeParameters_ReturnsVoidType()
    {
        // Arrange
        var functionDeclarationStatement = new FunctionDeclarationStatement
        {
            ReturnType = new TypeMock(),
            Name = new Token(TokenType.Identifier),
            Parameters = new ParameterList
            {
                Parameters = new[]
                {
                    new Parameter(new Token(TokenType.Identifier), new TypeMock()),
                    new Parameter(new Token(TokenType.Identifier), new TypeMock()),
                    new Parameter(new Token(TokenType.Identifier), new TypeMock())
                }
            }
        };

        var expectedType = new FunctionType
        (
            functionDeclarationStatement.ReturnType,
            new TypeList
            (
                functionDeclarationStatement.Parameters.Parameters.Select(parameter => parameter.Type).ToArray()
            )
        );
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            functionDeclarationStatement
        );
        
        // Assert
        Assert.Equal(new VoidType(), result);
        
        _semanticAnalyzerContextMock.Verify(x => x.DefineSymbol(It.Is<Symbol>(s => s.Type == expectedType)));
    }
}
