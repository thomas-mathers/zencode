using Xunit;
using ZenCode.Parser.Model.Grammar.Types;
using Sut = ZenCode.SemanticAnalysis.Analyzers.Statements.PrintStatementAnalyzer;

namespace ZenCode.SemanticAnalysis.Tests.Analyzers.Statements;

public class PrintStatementAnalyzerTests
{
    [Fact]
    public void Analyze()
    {
        // Assert + Arrange + Act
        Assert.Equal(new VoidType(), Sut.Analyze());
    }
}
