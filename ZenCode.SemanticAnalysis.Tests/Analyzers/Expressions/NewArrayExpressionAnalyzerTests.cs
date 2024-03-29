using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Expressions;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class NewArrayExpressionAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly NewArrayExpressionAnalyzer _sut = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                null!,
                new NewArrayExpression
                {
                    Type = new TypeMock(),
                    Size = new ExpressionMock()
                }
            )
        );
    }
    
    [Fact]
    public void Analyze_NullExpression_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => _sut.Analyze
            (
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }
    
    [Fact]
    public void Analyze_TypeMockExpressionMock_ReturnsCorrectType()
    {
        // Arrange
        var expected = new ArrayType { BaseType = new TypeMock() };
        
        // Act
        var result = _sut.Analyze
        (
            _semanticAnalyzerContextMock.Object,
            new NewArrayExpression
            {
                Type = new TypeMock(),
                Size = new ExpressionMock()
            }
        );
        
        // Assert
        Assert.Equal(expected, result);
    }
}
