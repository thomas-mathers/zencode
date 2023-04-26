using Xunit;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Analyzers.Statements;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class ReadStatementAnalyzerTests
{
    private readonly ReadStatementAnalyzer _sut = new();
    
    [Fact]
    public void Analyze_ReadStatement_ReturnsVoidType()
    {
        // Assert + Arrange + Act
        Assert.Equal(new VoidType(), _sut.Analyze());
    }
}
