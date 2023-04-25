using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.Tests.Common.Mocks;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Statements.IfStatementAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class IfStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange
        var ifStatement = new IfStatement
        {
            ThenScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };
        
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                null!,
                _semanticAnalyzerContextMock.Object,
                ifStatement
            )
        );
    }
    
    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange
        var ifStatement = new IfStatement
        {
            ThenScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };
        
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                null!,
                ifStatement
            )
        );
    }
    
    [Fact]
    public void Analyze_NullIfStatement_ThrowsArgumentNullException()
    {
        // Act + Assert
        Assert.Throws<ArgumentNullException>
        (
            () => Sut.Analyze
            (
                _semanticAnalyzerMock.Object,
                _semanticAnalyzerContextMock.Object,
                null!
            )
        );
    }
    
    [Fact]
    public void Analyze_IfStatement_ReturnsVoidType()
    {
        // Arrange
        var ifStatement = new IfStatement
        {
            ThenScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };
        
        // Act
        var result = Sut.Analyze
        (
            _semanticAnalyzerMock.Object,
            _semanticAnalyzerContextMock.Object,
            ifStatement
        );
        
        // Assert
        Assert.Equal(new VoidType(), result);
    }
}
