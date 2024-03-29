using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class ReturnStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly ReturnStatementAnalyzer _sut = new();

    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Analyze(null!, _semanticAnalyzerContextMock.Object, new()));
    }

    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Analyze(_semanticAnalyzerMock.Object, null!, new()));
    }

    [Fact]
    public void Analyze_NullReturnStatement_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, null!));
    }
    
    [Fact]
    public void Analyze_ReturnStatementOutsideFunction_AddsInvalidReturnException()
    {
        // Arrange
        var returnStatement = new ReturnStatement();
        
        // Act
        _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        _semanticAnalyzerContextMock.Verify(e => e.AddError(It.IsAny<InvalidReturnException>()), Times.Once);
    }
    
    [Fact]
    public void Analyze_ReturnFromFunctionWithVoidReturnType_ReturnsVoidType()
    {
        // Arrange
        var returnStatement = new ReturnStatement();
        
        var functionDeclaration = new FunctionDeclarationStatement
        {
            ReturnType = new VoidType(),
            Name = new Token(TokenType.Identifier, "f")
        };
        
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new[]
            {
                functionDeclaration
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        Assert.IsType<VoidType>(result);
    }
    
    [Fact]
    public void Analyze_ReturnFromFunctionWithBooleanReturnType_ReturnsVoidType()
    {
        // Arrange
        var returnStatement = new ReturnStatement
        {
            Value = new ExpressionMock()
        };
        
        var functionDeclaration = new FunctionDeclarationStatement
        {
            ReturnType = new BooleanType(),
            Name = new Token(TokenType.Identifier, "f")
        };
        
        _semanticAnalyzerMock
            .Setup(e => e.Analyze(_semanticAnalyzerContextMock.Object, returnStatement.Value))
            .Returns(new BooleanType());
        
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new[]
            {
                functionDeclaration
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        Assert.IsType<VoidType>(result);
    }
    
    [Fact]
    public void Analyze_ReturnFromFunctionWithIntegerType_ReturnsVoidType()
    {
        // Arrange
        var returnStatement = new ReturnStatement
        {
            Value = new ExpressionMock()
        };
        
        var functionDeclaration = new FunctionDeclarationStatement
        {
            ReturnType = new IntegerType(),
            Name = new Token(TokenType.Identifier, "f")
        };
        
        _semanticAnalyzerMock
            .Setup(e => e.Analyze(_semanticAnalyzerContextMock.Object, returnStatement.Value))
            .Returns(new IntegerType());
        
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new[]
            {
                functionDeclaration
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        Assert.IsType<VoidType>(result);
    }
    
    [Fact]
    public void Analyze_ReturnFromFunctionWithFloatType_ReturnsVoidType()
    {
        // Arrange
        var returnStatement = new ReturnStatement
        {
            Value = new ExpressionMock()
        };
        
        var functionDeclaration = new FunctionDeclarationStatement
        {
            ReturnType = new FloatType(),
            Name = new Token(TokenType.Identifier, "f")
        };
        
        _semanticAnalyzerMock
            .Setup(e => e.Analyze(_semanticAnalyzerContextMock.Object, returnStatement.Value))
            .Returns(new FloatType());
        
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new[]
            {
                functionDeclaration
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        Assert.IsType<VoidType>(result);
    }
    
    [Fact]
    public void Analyze_ReturnFromFunctionWithStringType_ReturnsVoidType()
    {
        // Arrange
        var returnStatement = new ReturnStatement
        {
            Value = new ExpressionMock()
        };
        
        var functionDeclaration = new FunctionDeclarationStatement
        {
            ReturnType = new StringType(),
            Name = new Token(TokenType.Identifier, "f")
        };
        
        _semanticAnalyzerMock
            .Setup(e => e.Analyze(_semanticAnalyzerContextMock.Object, returnStatement.Value))
            .Returns(new StringType());
        
        _semanticAnalyzerContextMock
            .Setup(e => e.AncestorAstNodes())
            .Returns(new[]
            {
                functionDeclaration
            });
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, returnStatement);
        
        // Assert
        Assert.IsType<VoidType>(result);
    }
}
