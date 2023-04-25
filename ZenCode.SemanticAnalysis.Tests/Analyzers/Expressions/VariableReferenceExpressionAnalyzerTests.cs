using Moq;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Exceptions;
using ZenCode.Tests.Common.Mocks;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Expressions.VariableReferenceExpressionAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class VariableReferenceExpressionAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                null!,
                new VariableReferenceExpression(new Token(TokenType.Identifier))
            )
        );
    }
    
    [Fact]
    public void Analyze_NullExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }
    
    [Fact]
    public void Analyze_ResolveSymbolReturnsNull_ThrowsUndeclaredIdentifierException()
    {
        // Arrange
        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));
        
        // Act + Assert
        Assert.Throws<UndeclaredIdentifierException>
        (
            () => Sut.Analyze(_semanticAnalyzerContextMock.Object, expression)
        );
    }
    
    [Fact]
    public void Analyze_ResolveSymbolReturnsType_SetsAstNodeType()
    {
        // Arrange
        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));
        var type = new TypeMock();
        
        _semanticAnalyzerContextMock
            .Setup(x => x.ResolveSymbol(expression.Identifier.Text))
            .Returns(new Symbol(expression.Identifier, type));
        
        // Act
        var result = Sut.Analyze(_semanticAnalyzerContextMock.Object, expression);
        
        // Assert
        Assert.Equal(type, result);
        
        _semanticAnalyzerContextMock.Verify(x => x.SetAstNodeType(expression, type), Times.Once);
    }
}
