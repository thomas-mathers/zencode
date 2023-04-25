using Xunit;
using ZenCode.Parser.Model.Grammar.Types;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Statements.ReadStatementAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class ReadStatementAnalyzerTests
{
    [Fact]
    public void Analyze()
    {
        // Assert + Arrange + Act
        Assert.Equal(new VoidType(), Sut.Analyze());
    }
}
