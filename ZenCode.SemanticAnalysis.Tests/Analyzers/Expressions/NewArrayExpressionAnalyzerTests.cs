using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.Tests.Common.Mocks;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Expressions.NewArrayExpressionAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Expressions;

public class NewArrayExpressionAnalyzerTests
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
                new NewArrayExpression(new TypeMock(), new ExpressionMock())
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
    public void Analyze_TypeMockExpressionMock_ReturnsCorrectType()
    {
        // Arrange
        var expected = new ArrayType(new TypeMock());
        
        // Act
        var result = Sut.Analyze
        (
            _semanticAnalyzerContextMock.Object,
            new NewArrayExpression(new TypeMock(), new ExpressionMock())
        );
        
        // Assert
        Assert.Equal(expected, result);
    }
}
