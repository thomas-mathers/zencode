using Xunit;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.SemanticAnalysis.Analyzers.Statements;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class PrintStatementAnalyzerTests
{
    private readonly PrintStatementAnalyzer _sut = new();
    
    [Fact]
    public void Analyze()
    {
        // Assert + Arrange + Act
        Assert.Equal(new VoidType(), _sut.Analyze());
    }
}
