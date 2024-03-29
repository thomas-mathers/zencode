using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class ScopeAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly ScopeAnalyzer _sut = new();

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
    public void Analyze_NullScope_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, null!));
    }
    
    [Fact]
    public void Analyze_ScopeWithNoStatements_ReturnsVoidType()
    {
        // Arrange
        var scope = new Scope();
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, scope);
        
        // Assert
        Assert.Equal(new VoidType(), result);
    }
    
    [Fact]
    public void Analyze_ScopeWithStatements_ReturnsVoidType()
    {
        // Arrange
        var scope = new Scope
        (
            new StatementMock(),
            new StatementMock(),
            new StatementMock()
        );
        
        // Act
        var result = _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, scope);
        
        // Assert
        Assert.Equal(new VoidType(), result);
    }
}
