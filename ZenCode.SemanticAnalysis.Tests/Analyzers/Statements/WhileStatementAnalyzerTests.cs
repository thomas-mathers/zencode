using Moq;
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.SemanticAnalysis.Abstractions;
using ZenCode.SemanticAnalysis.Analyzers.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class WhileStatementAnalyzerTests
{
    private readonly Mock<ISemanticAnalyzer> _semanticAnalyzerMock = new();
    private readonly Mock<ISemanticAnalyzerContext> _semanticAnalyzerContextMock = new();
    private readonly WhileStatementAnalyzer _sut = new();
    
    [Fact]
    public void Analyze_NullSemanticAnalyzer_ThrowsArgumentNullException()
    {
        // Arrange
        var whileStatement = new WhileStatement
        {
            ConditionScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };

        // Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(null!, _semanticAnalyzerContextMock.Object, whileStatement));
    }
    
    [Fact]
    public void Analyze_NullSemanticAnalyzerContext_ThrowsArgumentNullException()
    {
        // Arrange
        var whileStatement = new WhileStatement
        {
            ConditionScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };

        // Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, null!, whileStatement));
    }
    
    [Fact]
    public void Analyze_NullWhileStatement_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        Assert.Throws<ArgumentNullException>
            (() => _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, null!));
    }
    
    [Fact]
    public void Analyze_WhileStatement_CallsAnalyzeOnConditionScope()
    {
        // Arrange
        var whileStatement = new WhileStatement
        {
            ConditionScope = new ConditionScope
            {
                Condition = new ExpressionMock()
            }
        };

        // Act
        _sut.Analyze(_semanticAnalyzerMock.Object, _semanticAnalyzerContextMock.Object, whileStatement);

        // Assert
        _semanticAnalyzerMock.Verify
        (
            analyzer => analyzer.Analyze(_semanticAnalyzerContextMock.Object, whileStatement.ConditionScope),
            Times.Once
        );
    }
}
